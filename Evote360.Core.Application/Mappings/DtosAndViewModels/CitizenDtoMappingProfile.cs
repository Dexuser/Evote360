using AutoMapper;
using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.ViewModels.Citizen;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class CitizenDtoMappingProfile : Profile
{
    public CitizenDtoMappingProfile()
    {
        CreateMap<CitizenDto, CitizenViewModel>().ReverseMap();

        CreateMap<CitizenDto, CitizenSaveViewModel>()
            .ReverseMap();
        
        CreateMap<CitizenDto, CitizenChangeStateViewModel>();
    }
}