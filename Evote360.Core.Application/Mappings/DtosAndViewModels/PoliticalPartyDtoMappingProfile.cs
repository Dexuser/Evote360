using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.ViewModels.PoliticalParty;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class PoliticalPartyDtoMappingProfile : Profile 
{
    public PoliticalPartyDtoMappingProfile()
    {
        CreateMap<PoliticalPartyDto, PoliticalPartyViewModel>().ReverseMap();
        

        CreateMap<PoliticalPartyCreateViewModel, PoliticalPartyDto>()
            .ForMember(dest => dest.LogoPath, opt => opt.Ignore());
        
        CreateMap<PoliticalPartyUpdateViewModel, PoliticalPartyDto>()
            .ForMember(dest => dest.LogoPath, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.LogoFile, opt => opt.Ignore());
        
        CreateMap<PoliticalPartyDto, PoliticalPartyChangeStateViewModel>().ReverseMap();
    }
}