using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.Dtos.Election;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Interfaces;

public interface IElectionServices : IGenericServices<ElectionDto>
{
    Task<Result<List<ElectionSummaryDto>>> GetSummaryOfAllElections();
    Task<Result<List<ElectionSummaryCandidateDto>>> GetSummaryOfElectionsWithCandidates(int year);
    Task<Result<List<int>>> GetYearsWithElections();
}