using System.Reflection;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.Services;
using Evote360.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Evote360.Core.Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationLayerIoc(this IServiceCollection services)
    {
        #region Configurations 
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        #endregion
        
        #region Services IOC

        //services.AddScoped(typeof(IGenericService<>), typeof(GenericService<,>));
        services.AddScoped<ICandidatePositionServices, CandidatePositionServices>();
        services.AddScoped<ICandidateServices, CandidateServices>();
        services.AddScoped<ICitizenServices, CitizenServices>();
        services.AddScoped<IElectionServices, ElectionServices>();
        services.AddScoped<IElectivePositionServices, ElectivePositionServices>();
        services.AddScoped<IPartyLeaderAssignmentServices, PartyLeaderAssignmentServices>();
        services.AddScoped<IPoliticalAllianceServices, PoliticalAllianceServices>();
        services.AddScoped<IPoliticalPartyServices, PoliticalPartyServices>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IVoteServices, VoteServices>();

        #endregion

        return services;
    }
}