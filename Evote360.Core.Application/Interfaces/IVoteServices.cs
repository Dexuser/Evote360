using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Dtos.Vote;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Interfaces;

public interface IVoteServices : IGenericServices<VoteDto>
{
}