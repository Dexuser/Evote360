using AutoMapper;
using Evote360.Core.Application.Dtos.PartyLeaderAssignment;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Mappings.EntitiesAndDtos;

public class PartyLeaderAssignmentMappingProfile : Profile
{
    public PartyLeaderAssignmentMappingProfile()
    {
        CreateMap<PartyLeaderAssignment, PartyLeaderAssignmentDto>().ReverseMap();
    }
}