using AutoMapper;
using Evote360.Core.Application.Dtos.CandidatePosition;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class CandidatePositionServices : GenericServices<CandidatePosition, CandidatePositionDto>,
    ICandidatePositionServices
{
    private readonly IMapper _mapper;
    private readonly ICandidatePositionRepository _candidatePositionRepository;
    private readonly IElectivePositionRepository _electivePositionRepository;
    private readonly ICandidateRepository _candidateRepository;

    public CandidatePositionServices(ICandidatePositionRepository repository, IMapper mapper,
        IElectivePositionRepository electivePositionRepository,
        ICandidateRepository candidateRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _electivePositionRepository = electivePositionRepository;
        _candidateRepository = candidateRepository;
        _candidatePositionRepository = repository;
    }


    public override async Task<Result<CandidatePositionDto>> AddAsync(CandidatePositionDto dtoModel)
    {
        // Verificar que existan las entidades
        var candidate = await _candidateRepository.GetAllQueryable()
            .Include(c => c.CandidatePositions)
            .FirstOrDefaultAsync(c => c.Id == dtoModel.CandidateId);

        if (candidate == null)
            return Result<CandidatePositionDto>.Fail("El candidato especificado no existe.",
                messageType: MessageType.Alert);

        var positionExistsInParty = await _candidatePositionRepository.GetAllQueryable()
            .AnyAsync(cp => cp.PoliticalPartyId == dtoModel.PoliticalPartyId &&
                            cp.ElectivePositionId == dtoModel.ElectivePositionId);

        if (positionExistsInParty)
            return Result<CandidatePositionDto>.Fail("Este puesto ya tiene un candidato asignado en este partido.",
                messageType: MessageType.Alert);

        //  Validación: candidato del mismo partido
        if (candidate.PoliticalPartyId == dtoModel.PoliticalPartyId)
        {
            bool alreadyAssignedInSameParty = candidate.CandidatePositions
                .Any(cp => cp.PoliticalPartyId == dtoModel.PoliticalPartyId);

            if (alreadyAssignedInSameParty)
                return Result<CandidatePositionDto>.Fail(
                    "El candidato ya tiene un puesto asignado dentro de este partido.",
                    messageType: MessageType.Alert);
        }
        else
        {
            // Validación: candidato de partido aliado debe tener el mismo puesto en su partido de origen
            bool hasSamePositionInOrigin = candidate.CandidatePositions
                .Any(cp => cp.PoliticalPartyId == candidate.PoliticalPartyId &&
                           cp.ElectivePositionId == dtoModel.ElectivePositionId);

            if (!hasSamePositionInOrigin)
                return Result<CandidatePositionDto>.Fail(
                    "El candidato aliado debe aspirar al mismo puesto que en su partido de origen.",
                    messageType: MessageType.Alert);
        }

        return await base.AddAsync(dtoModel);
    }


    public async Task<Result<List<CandidatePositionDto>>> GetAllTheCandidatesAssignedByThisPartyAsync(
        int politicalPartyId)
    {
        var query = await _candidatePositionRepository.GetAllQueryable().AsNoTracking()
            .Include(cp => cp.Candidate)
            .Include(cp => cp.ElectivePosition)
            .Where(cp => cp.PoliticalPartyId == politicalPartyId).ToListAsync();

        return Result<List<CandidatePositionDto>>.Ok(_mapper.Map<List<CandidatePositionDto>>(query));
    }


    public async Task<Result<List<ElectivePositionDto>>> GetAllTheAvailableElectivePositionsForThisPartyAsync(
        int politicalPartyId)
    {
        var occupiedPositionsIds = await _candidatePositionRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(cp => cp.PoliticalPartyId == politicalPartyId)
            .Select(cp => cp.ElectivePositionId)
            .Distinct()
            .ToListAsync();

        var availablePositions = await _electivePositionRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(e => e.IsActive && !occupiedPositionsIds.Contains(e.Id))
            .ToListAsync();

        return Result<List<ElectivePositionDto>>.Ok(_mapper.Map<List<ElectivePositionDto>>(availablePositions));
    }
}