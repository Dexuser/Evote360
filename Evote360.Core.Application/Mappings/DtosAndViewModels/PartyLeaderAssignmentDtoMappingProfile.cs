using AutoMapper;
using Evote360.Core.Application.Dtos.PartyLeaderAssignment;
using Evote360.Core.Application.ViewModels.PartyLeaderAssignment;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class PartyLeaderAssignmentDtoMappingProfile : Profile
{
    public PartyLeaderAssignmentDtoMappingProfile()
    {
        CreateMap<PartyLeaderAssignmentDto, PartyLeaderAssignmentViewModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
            .ForMember(dest => dest.PoliticalPartyAcronym, opt => opt.MapFrom(src =>
                src.PoliticalParty != null ? src.PoliticalParty.Acronym : string.Empty));


        CreateMap<PartyLeaderAssignmentCreateViewModel, PartyLeaderAssignmentDto>()
            .ReverseMap();
    }
}