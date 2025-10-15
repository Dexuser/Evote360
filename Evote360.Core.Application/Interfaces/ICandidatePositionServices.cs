using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.CandidatePosition;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Interfaces;

public interface ICandidatePositionServices : IGenericServices<CandidatePositionDto> 
{
    Task<Result<List<CandidatePositionDto>>> GetAllTheCandidatesAssignedByThisPartyAsync(int politicalPartyId);
    Task<Result<List<ElectivePositionDto>>> GetAllTheAvailableElectivePositionsForThisPartyAsync(int politicalPartyId);
    
}