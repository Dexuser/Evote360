using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class CandidateMappingProfile : Profile
{
    public CandidateMappingProfile()
    {
        CreateMap<Candidate, CandidateDto>()
            .ForMember(dest => dest.ElectivePosition, opt => opt.Ignore()) // proyectamos en el servicio
            .ReverseMap();

    }
}