using AutoMapper;
using Evote360.Core.Application.Dtos;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Services;

public class ElectivePositionServices : GenericServices<ElectivePosition, ElectivePositionDto>, IElectivePositionServices 
{
    private readonly IMapper _mapper;
    private readonly IElectivePositionRepository _electivePositionRepository;
    
    public ElectivePositionServices(IElectivePositionRepository repository, IMapper mapper ) : base(repository, mapper)
    {
        _mapper = mapper;
        _electivePositionRepository = repository;
    }
    public async Task SetActiveAsync(int id, bool isActive)
    {
        await _electivePositionRepository.SetActiveAsync(id, isActive);
    }

    public async Task<List<ElectivePositionDto>> GetAllTActiveAsync()
    {
        var entities = await _electivePositionRepository.GetAllTActiveAsync();
        return _mapper.Map<List<ElectivePositionDto>>(entities);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _electivePositionRepository.IsActiveAsync(id);
    }
}