using AutoMapper;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Dtos.Vote;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Services;

public class VoteServices : GenericServices<Vote, VoteDto>, IVoteServices 
{
    private readonly IMapper _mapper;
    private readonly IVoteRepository _repository;
    
    public VoteServices(IVoteRepository repository, IMapper mapper ) : base(repository, mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
}