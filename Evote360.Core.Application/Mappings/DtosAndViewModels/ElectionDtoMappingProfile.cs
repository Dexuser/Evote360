using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.Election;
using Evote360.Core.Application.ViewModels.Candidate;
using Evote360.Core.Application.ViewModels.Election;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class ElectionDtoMappingProfile : Profile
{
    public ElectionDtoMappingProfile()
    {
        CreateMap<ElectionDto, ElectionViewModel>();

        CreateMap<ElectionSummaryCandidateDto, ElectionSummaryCandidateViewModel>().ReverseMap();

        CreateMap<ElectionSummaryDto, ElectionSummaryViewModel>().ReverseMap();

        CreateMap<ElectionCreateViewModel, ElectionDto>().ReverseMap();

    }
}