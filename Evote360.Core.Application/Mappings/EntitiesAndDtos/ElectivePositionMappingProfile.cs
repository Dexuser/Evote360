using AutoMapper;
using Evote360.Core.Application.Dtos;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class ElectivePositionMappingProfile : Profile
{
    public ElectivePositionMappingProfile()
    {
        CreateMap<ElectivePosition,ElectivePositionDto >().ReverseMap();
    }
}