using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class CandidateServices : GenericServices<Candidate, CandidateDto>, ICandidateServices 
{
    private readonly IMapper _mapper;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IPartyLeaderAssignmentRepository _partyLeaderAssignmentRepository;
    private readonly IPoliticalAllianceRepository _allianceRepository;

    public CandidateServices(ICandidateRepository repository, IMapper mapper,
        IPartyLeaderAssignmentRepository partyLeaderAssignmentRepository,
        IPoliticalAllianceRepository allianceRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _partyLeaderAssignmentRepository = partyLeaderAssignmentRepository;
        _allianceRepository = allianceRepository;
        _candidateRepository = repository;
    }
    
    
    public async Task SetActiveAsync(int id, bool isActive)
    {
        await _candidateRepository.SetActiveAsync(id, isActive);
    }

    public async Task<List<CandidateDto>> GetAllTActiveAsync()
    {
        var entities = await _candidateRepository.GetAllTActiveAsync();
        return _mapper.Map<List<CandidateDto>>(entities);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _candidateRepository.IsActiveAsync(id);
    }


    // Trae todos los candidatos que FUERON AGREGADOS AL SISTEMA POR ESTE PARTIDO
    public async Task<Result<List<CandidateDto>>> GetAllTheCandidatesOfThisPoliticalPartyAsync(int politicalPartyId)
    {
        var candidates = await _candidateRepository.GetAllQueryable()
            .Where(x => x.PoliticalPartyId == politicalPartyId)
            .ToListAsync();
        
        return Result<List<CandidateDto>>.Ok(_mapper.Map<List<CandidateDto>>(candidates));
    }

    
    public async Task<Result<List<CandidateDto>>> GetAllTheCandidatesAndAllyCandidatesOfThisPoliticalPartyAsync(int politicalPartyId)
    {
        
        //  Obtener alianzas del partido actual
        var alliances = await _allianceRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(a => a.RequestingPartyId == politicalPartyId || a.TargetPartyId == politicalPartyId)
            .ToListAsync();

        var alliedPartyIds = alliances
            .Select(a => a.RequestingPartyId == politicalPartyId ? a.TargetPartyId : a.RequestingPartyId)
            .Distinct()
            .ToList();

        // Candidatos activos del partido y aliados
        var query = _candidateRepository.GetAllQueryable()
            .AsNoTracking()
            .Include(c => c.CandidatePositions)
            .Where(c => c.IsActive &&
                        (c.PoliticalPartyId == politicalPartyId || alliedPartyIds.Contains(c.PoliticalPartyId)));

        query = query.Where(c =>
            // Si es del partido actual no debe tener ningún puesto en su propio partido
            (c.PoliticalPartyId == politicalPartyId &&
             !c.CandidatePositions.Any(cp => cp.PoliticalPartyId == politicalPartyId))

            // Si es aliado no importa si tiene puesto, lo filtramos después al elegir puesto
            || alliedPartyIds.Contains(c.PoliticalPartyId)
        );

        var result = await query.ToListAsync();
        return Result<List<CandidateDto>>.Ok(_mapper.Map<List<CandidateDto>>(result));
    }
}