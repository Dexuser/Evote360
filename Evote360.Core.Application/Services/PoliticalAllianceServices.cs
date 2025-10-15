using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalAlliance;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class PoliticalAllianceServices : GenericServices<PoliticalAlliance, PoliticalAllianceDto>, IPoliticalAllianceServices 
{
    private readonly IMapper _mapper;
    private readonly IPoliticalAllianceRepository _allianceRepository;
    private readonly IPoliticalPartyRepository _partyRepository;
    
    public PoliticalAllianceServices(IPoliticalAllianceRepository repository, IMapper mapper, IPoliticalPartyRepository partyRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _partyRepository = partyRepository;
        _allianceRepository = repository;
    }

    // Ojo, Esto es para controlar la Simetria. La relacion del partido 1 con el partido 2 (1,2) es la misma que (2, 1)
    public override async Task<Result<PoliticalAllianceDto>> AddAsync(PoliticalAllianceDto dtoModel)
    {
        bool thereIsADuplicate  = await _allianceRepository.GetAllQueryable().AsNoTracking()
            .AnyAsync(pa => pa.TargetPartyId == dtoModel.TargetPartyId && 
                            pa.RequestingPartyId == dtoModel.RequestingPartyId ||
                            pa.TargetPartyId ==  dtoModel.RequestingPartyId && 
                            pa.RequestingPartyId == dtoModel.TargetPartyId);

        if (thereIsADuplicate)
        {
            return Result<PoliticalAllianceDto>.Fail(
                "Ya existe una solicitud entre estos dos partidos." +
                " Revise si ha sido aceptada o esta en espera. ",
                messageType: MessageType.Alert);
        }
        
        dtoModel.RequestDate = DateTime.Now;
        
        return await base.AddAsync(dtoModel);
    }

    // Todos los registros en donde soy el soliciante y el tarqet y que no esten rechazadas
    public async Task<Result<List<PoliticalAllianceDto>>> GetAllTheRequestsOfThisPoliticalParty(int politicalPartyId)
    {
        var request = await _allianceRepository.GetAllQueryable().AsNoTracking()
            .Include(alliance => alliance.RequestingParty)
            .Include(alliance => alliance.TargetParty)
            .Where(request =>
                request.Status != AllianceStatus.Rejected && 
                (request.RequestingPartyId == politicalPartyId || 
                request.TargetPartyId == politicalPartyId))
            .ToListAsync();
        
        return Result<List<PoliticalAllianceDto>>.Ok(_mapper.Map<List<PoliticalAllianceDto>>(request));
    }

    public async Task<Result<List<PoliticalPartyDto>>> GetAllTheAvailablePoliticalParties(int politicalPartyId)
    {
        var alliances = await _allianceRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(a => a.RequestingPartyId == politicalPartyId || a.TargetPartyId == politicalPartyId)
            .ToListAsync();

        var alliedPartyIds = alliances
            .Select(a => a.RequestingPartyId == politicalPartyId ? a.TargetPartyId : a.RequestingPartyId)
            .Distinct()
            .ToList();

        var availableParties = await _partyRepository.GetAllQueryable()
            .AsNoTracking()
            .Where(p => p.Id != politicalPartyId && !alliedPartyIds.Contains(p.Id))
            .ToListAsync();

        return Result<List<PoliticalPartyDto>>.Ok(_mapper.Map<List<PoliticalPartyDto>>(availableParties));
            
    }

    public async Task<Result> AcceptThisAllianceRequest(int politicalAllianceId, bool accept)
    {
        try
        {
            await _allianceRepository.AcceptThisAllianceRequest(politicalAllianceId, accept);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}