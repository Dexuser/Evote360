using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.CandidatePosition;
using Evote360.Core.Application.ViewModels.Candidate;
using Evote360.Core.Application.ViewModels.CandidatePosition;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class CandidatePositionDtoMappingProfile : Profile
{
    public CandidatePositionDtoMappingProfile()
    {
        CreateMap<CandidatePositionDto, CandidatePositionViewModel>().ReverseMap();
        CreateMap<CandidatePositionDto, CandidatePositionCreateViewModel>().ReverseMap();

    }
}