using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalAlliance;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class PoliticalAllianceMappingProfile : Profile
{
    public PoliticalAllianceMappingProfile()
    {
        CreateMap<PoliticalAlliance, PoliticalAllianceDto>().ReverseMap();
    }
}