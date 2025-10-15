using AutoMapper;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.ViewModels.ElectivePosition;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class ElectivePositionDtoMappingProfile : Profile
{
    public ElectivePositionDtoMappingProfile()
    {
        CreateMap<ElectivePositionDto, ElectivePositionViewModel>().ReverseMap();
        
        CreateMap<ElectivePositionDto, ElectivePositionChangeStateViewModel>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Description, opt => opt.Ignore());
        
        CreateMap<ElectivePositionSaveViewModel, ElectivePositionDto>()
            .ReverseMap();;
    }
}