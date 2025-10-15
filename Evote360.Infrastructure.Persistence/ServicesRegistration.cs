using System.Reflection;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Evote360.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evote360.Infrastructure.Persistence;

public static class ServicesRegistration
{
    public static IServiceCollection AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
    {
        #region Context

        services.AddDbContext<VoteContext>(options => options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(VoteContext).Assembly.FullName)
            )
        );

        #endregion

        #region Repositories

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<ICandidatePositionRepository, CandidatePositionRepository>();
        services.AddScoped<ICitizenRepository, CitizenRepository>();
        services.AddScoped<IElectionRepository, ElectionRepository>();
        services.AddScoped<IElectivePositionRepository, ElectivePositionRepository>();
        services.AddScoped<IPartyLeaderAssignmentRepository, PartyLeaderAssignmentRepository>();
        services.AddScoped<IPoliticalAllianceRepository, PoliticalAllianceRepository>();
        services.AddScoped<IPoliticalPartyRepository, PoliticalPartyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVoteRepository, VoteRepository>();
        services.AddScoped<IElectionCandidateRepository, ElectionCandidateRepository>();

        services.AddScoped<IElectionPositionRepository, ElectionPositionRepository>();
        services.AddScoped<IElectionCandidateRepository, ElectionCandidateRepository>();
        services.AddScoped<IElectionPartyRepository, ElectionPartyRepository>();
        #endregion

        return services;
    }
}