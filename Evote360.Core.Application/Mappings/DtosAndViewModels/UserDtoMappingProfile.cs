using AutoMapper;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.ViewModels.User;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class UserDtoMappingProfile : Profile 
{
    public UserDtoMappingProfile()
    {
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        
        CreateMap<UserDto, UserChangeStateViewModel>();
        
        CreateMap<UserCreateViewModel, UserDto>().ReverseMap();
        CreateMap<UserUpdateViewModel, UserDto>().ReverseMap();
    }
}