using System.Reflection;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Context;

public class VoteContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<CandidatePosition> CandidatePositions { get; set; }
    public DbSet<Election> Elections { get; set; }
    public DbSet<ElectivePosition> ElectivePositions { get; set; }
    public DbSet<PartyLeaderAssignment> PartyLeaderAssignments { get; set; }
    public DbSet<PoliticalAlliance> PoliticalAlliances { get; set; }
    public DbSet<PoliticalParty> PoliticalParties { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public VoteContext(DbContextOptions<VoteContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}