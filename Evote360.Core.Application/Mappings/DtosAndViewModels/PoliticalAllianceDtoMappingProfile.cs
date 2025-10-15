using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalAlliance;
using Evote360.Core.Application.ViewModels.PoliticalAlliance;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.Mappings.DtosAndViewModels;

public class PoliticalAllianceDtoMappingProfile : Profile
{
    public PoliticalAllianceDtoMappingProfile()
    {
        CreateMap<PoliticalAllianceDto, PoliticalAllianceViewModel>().ReverseMap();

        
        CreateMap<PoliticalAllianceDto, PoliticalAllianceCreateViewModel>().ReverseMap();

        CreateMap<PoliticalAllianceDto, PoliticalAllianceDeleteViewModel>();

    }
}