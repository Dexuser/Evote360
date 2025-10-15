using AutoMapper;
using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Domain;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class CitizenMappingProfile : Profile
{
    public CitizenMappingProfile()
    {
        CreateMap<Citizen, CitizenDto>().ReverseMap();

    }
}