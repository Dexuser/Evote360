using AutoMapper;
using Evote360.Core.Application.Dtos.CandidatePosition;
using Evote360.Core.Application.ViewModels.CandidatePosition;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class CandidatePositionMappingProfile : Profile
{
    public CandidatePositionMappingProfile()
    {
        CreateMap<CandidatePosition,CandidatePositionDto>().ReverseMap();

    }
}