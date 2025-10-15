using System.Text;
using AutoMapper;
using Evote360.Core.Application.Dtos.Election;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class ElectionServices : GenericServices<Election, ElectionDto>, IElectionServices
{
    private readonly IMapper _mapper;
    private readonly IElectionRepository _repository;
    private readonly IElectionCandidateRepository _electionCandidateRepository;
    private readonly IElectionPartyRepository _electionPartyRepository;
    private readonly IElectionPositionRepository _electionPositionRepository;
    private readonly IPoliticalPartyRepository _politicalPartyRepository;
    private readonly ICandidatePositionRepository _candidatePositionRepository;
    private readonly IElectivePositionRepository _electivePositionRepository;
    private readonly IVoteRepository _voteRepository;

    public ElectionServices(IGenericRepository<Election> repository, IMapper mapper, IElectionRepository repository2,
        IElectionCandidateRepository electionCandidateRepository, IElectionPartyRepository electionPartyRepository,
        IElectionPositionRepository electionPositionRepository, IPoliticalPartyRepository politicalPartyRepository,
        ICandidatePositionRepository candidatePositionRepository,
        IElectivePositionRepository electivePositionRepository, IVoteRepository voteRepository) : base(repository,
        mapper)
    {
        _mapper = mapper;
        _repository = repository2;
        _electionCandidateRepository = electionCandidateRepository;
        _electionPartyRepository = electionPartyRepository;
        _electionPositionRepository = electionPositionRepository;
        _politicalPartyRepository = politicalPartyRepository;
        _candidatePositionRepository = candidatePositionRepository;
        _electivePositionRepository = electivePositionRepository;
        _voteRepository = voteRepository;
    }

    public override async Task<Result<ElectionDto>> AddAsync(ElectionDto dtoModel)
    {
        var activeElectivePositions = await _electivePositionRepository.GetAllQueryable().AsNoTracking()
            .Where(ep => ep.IsActive)
            .ToListAsync();
        
        if (!(activeElectivePositions.Count >= 1))
        {
            return Result<ElectionDto>.Fail("No hay suficientes puestos electivos para realizar una elección", messageType: MessageType.Alert);
        }
        
        var politicalParties = await _politicalPartyRepository.GetAllQueryable().AsNoTracking()
            .Where(p => p.IsActive)
            .ToListAsync();

        if (!(politicalParties.Count >= 2))
        {
            return Result<ElectionDto>.Fail("No hay suficientes partidos politicos para una elección", messageType: MessageType.Alert);
        }
        
        StringBuilder errorBuilderString = new StringBuilder();
        foreach (var politicalParty in politicalParties)
        {
            bool wasThePartyNameAdded = false;
            foreach (var electivePosition in activeElectivePositions)
            {
                bool haveACandidateAssigned = 
                    await _candidatePositionRepository.GetAllQueryable().AnyAsync(cp =>
                    cp.PoliticalPartyId == politicalParty.Id && cp.ElectivePositionId == electivePosition.Id);

                if (!haveACandidateAssigned)
                {
                    if (!wasThePartyNameAdded)
                    {
                        errorBuilderString.Append(
                            $"El partido politico {politicalParty.Name} {politicalParty.Acronym} " +
                            $"no tiene candidatos asignados para los puestos: ");
                        wasThePartyNameAdded = true;
                    }
                    errorBuilderString.Append(electivePosition.Name);
                }
            }
            errorBuilderString.AppendLine();
        }
        
        string errorString = errorBuilderString.ToString();
        
        if (!String.IsNullOrEmpty(errorString))
        {
            return Result<ElectionDto>.Fail(errorString, messageType: MessageType.Alert);    
        }
        
        var result = await base.AddAsync(dtoModel);

        if (result.IsFailure)
        {
            return result;
        }
        
        await RegisterParticipatingCandidates(result.Value!.Id);
        await RegisterParticipatingPoliticalParties(result.Value!.Id);
        await RegisterTheElectivePositions(result.Value!.Id);
        return result;
    }

    private async Task RegisterParticipatingPoliticalParties(int electionId)
    {
        var politicalPartiesId = await _politicalPartyRepository
            .GetAllQueryable()
            .AsNoTracking()
            .Where(p => p.IsActive).Select(p => p.Id)
            .ToListAsync();

        List<ElectionParty> electionParties = new List<ElectionParty>();
        foreach (var partyId in politicalPartiesId)
        {
            electionParties.Add(new ElectionParty { ElectionId = electionId, PoliticalPartyId = partyId });
        }

        await _electionPartyRepository.AddRangeAsync(electionParties);
    }

    private async Task RegisterParticipatingCandidates(int electionId)
    {
        var currentCandidatesPosition = await _candidatePositionRepository
            .GetAllQueryable()
            .AsNoTracking()
            .ToListAsync();

        List<ElectionCandidate> electionCandidates = new List<ElectionCandidate>();
        foreach (var candidate in currentCandidatesPosition)
        {
            electionCandidates.Add(new ElectionCandidate()
            {
                Id = 0,
                CandidateId = candidate.Id,
                PoliticalPartyId = candidate.PoliticalPartyId,
                ElectionId = electionId,
                ElectivePositionId = candidate.ElectivePositionId,
            });
        }

        await _electionCandidateRepository.AddRangeAsync(electionCandidates);
    }

    private async Task RegisterTheElectivePositions(int electionId)
    {
        var electivePositionsId = await _electivePositionRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => p.Id)
            .ToListAsync();
        
        List<ElectionPosition> electionPositions = new List<ElectionPosition>();
        foreach (var id in electivePositionsId)
        {
            electionPositions.Add(new ElectionPosition
            {
                Id = 0,
                ElectionId = electionId,
                ElectivePositionId = id,
            });
        }
        await _electionPositionRepository.AddRangeAsync(electionPositions);
    }


    public async Task<Result<List<ElectionSummaryDto>>> GetSummaryOfAllElections()
    {
        var election = await _repository.GetAllQueryable().AsNoTracking()
            .OrderByDescending(e => e.Status == ElectionStatus.InProgress)
            .ThenByDescending(e => e.Date)
            .Select(e => new ElectionSummaryDto
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date,
                Status = e.Status,
                ElectivePositionsCount = e.ElectionPositions.Count,
                PoliticalPartiesCount = e.ElectionParties.Count,
            }).ToListAsync();

        return Result<List<ElectionSummaryDto>>.Ok(election);
    }

    public async Task<Result<List<ElectionSummaryCandidateDto>>> GetSummaryOfElectionsWithCandidates(int year)
    {
        var election = await _repository.GetAllQueryable().AsNoTracking()
            .Where(e => e.Date.Year == year)
            .Select(e => new ElectionSummaryCandidateDto()
            {
                Id = e.Id,
                Name = e.Name,
                CandidatesCount = e.ElectionCandidates
                    .Select(e => e.CandidateId)
                    .Distinct()
                    .Count(),

                VotesCount = e.Votes.Select(v => v.CandidateId).Distinct().Count(),

                PoliticalPartyCount = e.ElectionParties.Count(),
            }).ToListAsync();
        return Result<List<ElectionSummaryCandidateDto>>.Ok(election);
    }

    public async Task<Result<List<int>>> GetYearsWithElections()
    {
        var query = await _repository.GetAllQueryable().AsNoTracking()
            .Select(e => e.Date.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToListAsync();
        return Result<List<int>>.Ok(query);
    }
}