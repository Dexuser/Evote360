using AutoMapper;
using Evote360.Core.Application.Dtos.Election;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class ElectionMappingProfile : Profile
{
    public ElectionMappingProfile()
    {
        CreateMap<Election, ElectionDto>()
            .ReverseMap()
            .ForMember(dest => dest.ElectionCandidates, opt => opt.Ignore())
            .ForMember(dest => dest.ElectionParties, opt => opt.Ignore())
            .ForMember(dest => dest.ElectionPositions, opt => opt.Ignore());
    }
}