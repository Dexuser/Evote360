using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Domain;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class PoliticalPartyMappingProfile : Profile 
{
    public PoliticalPartyMappingProfile()
    {
        CreateMap<PoliticalParty, PoliticalPartyDto>().ReverseMap();
    }
}