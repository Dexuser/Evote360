using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.ViewModels.Candidate;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class CandidateDtoMappingProfile : Profile
{
    public CandidateDtoMappingProfile()
    {
        CreateMap<CandidateDto, CandidateViewModel>().ReverseMap();

        CreateMap<CandidateDto, CandidateChangeStateViewModel>();

        CreateMap<CandidateCreateViewModel, CandidateDto>()
            .ForMember(dest => dest.PhotoPath, opt => opt.Ignore());
        
        CreateMap<CandidateUpdateViewModel, CandidateDto>()
            .ForMember(dest => dest.PhotoPath, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.PhotoFile, opt => opt.Ignore());


    }
}