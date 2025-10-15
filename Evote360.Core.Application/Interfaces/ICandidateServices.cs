using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Interfaces;

public interface ICandidateServices : IGenericServices<CandidateDto>,  IStateService<CandidateDto>
{
    Task<Result<List<CandidateDto>>> GetAllTheCandidatesOfThisPoliticalPartyAsync(int politicalPartyId);
    Task<Result<List<CandidateDto>>> GetAllTheCandidatesAndAllyCandidatesOfThisPoliticalPartyAsync(int politicalPartyId);
    
}