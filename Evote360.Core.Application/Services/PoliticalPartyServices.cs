using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class PoliticalPartyServices : GenericServices<PoliticalParty, PoliticalPartyDto>, IPoliticalPartyServices 
{
    private readonly IMapper _mapper;
    private readonly IPoliticalPartyRepository _repository;
    
    public PoliticalPartyServices(IPoliticalPartyRepository repository , IMapper mapper ) : base(repository, mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public override async Task<Result<PoliticalPartyDto>> AddAsync(PoliticalPartyDto dtoModel)
    {
        if (await _repository.ThisAcronymExistsAsync(dtoModel.Acronym, dtoModel.Id))
        {
            return Result<PoliticalPartyDto>.Fail(
                error: "Este acronimo ya esta registrado en el sistema",
                fieldName: nameof(dtoModel.Acronym),
                MessageType.Field
            );
        }
        
        return await base.AddAsync(dtoModel);
    }

    public override async Task<Result<PoliticalPartyDto>> UpdateAsync(int id, PoliticalPartyDto dtoModel)
    {
        if (await _repository.ThisAcronymExistsAsync(dtoModel.Acronym, dtoModel.Id))
        {
            return Result<PoliticalPartyDto>.Fail(
                error: "Este acronimo ya esta registrado en el sistema",
                fieldName: nameof(dtoModel.Acronym),
                MessageType.Field
            );
        }
        
        return await base.UpdateAsync(id, dtoModel);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        await _repository.SetActiveAsync(id, isActive);
    }

    public async Task<Result<List<PoliticalPartyDto>>> GetAllActivePoliticalParties()
    {
        try
        {
            var activeParties = await _repository.GetAllQueryable()
                .AsNoTracking()
                .Where(pl => pl.IsActive)
                .ToListAsync();
        
            return Result<List<PoliticalPartyDto>>.Ok(_mapper.Map<List<PoliticalPartyDto>>(activeParties));

        }
        catch (Exception e)
        {
            return Result<List<PoliticalPartyDto>>.Fail(e.Message, messageType: MessageType.Alert);
        }
    }
    
    public async Task<List<PoliticalPartyDto>> GetAllTActiveAsync()
    {
        var entities = await _repository.GetAllTActiveAsync();
        return _mapper.Map<List<PoliticalPartyDto>>(entities);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _repository.IsActiveAsync(id);
    }
}