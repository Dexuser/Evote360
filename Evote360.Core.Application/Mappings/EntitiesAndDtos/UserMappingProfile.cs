using AutoMapper;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class UserMappingProfile : Profile 
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}