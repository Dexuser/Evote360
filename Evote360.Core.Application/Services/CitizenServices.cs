using AutoMapper;
using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Application;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Services;

public class CitizenServices : GenericServices<Citizen, CitizenDto>, ICitizenServices 
{
    private readonly IMapper _mapper;
    private readonly ICitizenRepository _citizenRepository;


    public CitizenServices(ICitizenRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _mapper = mapper;
        _citizenRepository = repository;
    }

    public override async Task<Result<CitizenDto>> AddAsync(CitizenDto dtoModel)
    {
        if (await _citizenRepository.ThisIdentityNumberExistsAsync(dtoModel.IdentityNumber, dtoModel.Id))
        {
            return Result<CitizenDto>.Fail(
                error: "Este numero de identidad ya existe en el sistema",
                fieldName: nameof(CitizenDto.IdentityNumber));
        }
        
        if (await _citizenRepository.ThisEmailExistsAsync(dtoModel.Email, dtoModel.Id))
        {
            return Result<CitizenDto>.Fail(
                error: "Este correo ya existe en el sistema",
                fieldName: nameof(CitizenDto.Email));
        }

        return await base.AddAsync(dtoModel);
    }

    public override async Task<Result<CitizenDto>> UpdateAsync(int id, CitizenDto dtoModel)
    {
        if (await _citizenRepository.ThisIdentityNumberExistsAsync(dtoModel.IdentityNumber, dtoModel.Id))
        {
            return Result<CitizenDto>.Fail(
                error: "Este numero de identidad ya existe en el sistema",
                fieldName: nameof(CitizenDto.IdentityNumber));
        }
        
        if (await _citizenRepository.ThisEmailExistsAsync(dtoModel.Email, dtoModel.Id))
        {
            return Result<CitizenDto>.Fail(
                error: "Este correo ya existe en el sistema",
                fieldName: nameof(CitizenDto.Email));
        }
        
        return await base.UpdateAsync(id, dtoModel);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        await _citizenRepository.SetActiveAsync(id, isActive);
    }
    
    public async Task<List<CitizenDto>> GetAllTActiveAsync()
    {
        var entities = await _citizenRepository.GetAllTActiveAsync();
        return _mapper.Map<List<CitizenDto>>(entities);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _citizenRepository.IsActiveAsync(id);
    }
}