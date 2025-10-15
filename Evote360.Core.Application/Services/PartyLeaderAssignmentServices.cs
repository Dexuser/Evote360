using AutoMapper;
using Evote360.Core.Application.Dtos.PartyLeaderAssignment;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Core.Application.Services;

public class PartyLeaderAssignmentServices : GenericServices<PartyLeaderAssignment, PartyLeaderAssignmentDto>,
    IPartyLeaderAssignmentServices
{
    private readonly IMapper _mapper;
    private readonly IPartyLeaderAssignmentRepository _partyLeaderAssignmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPoliticalPartyRepository _politicalPartyRepository;

    public PartyLeaderAssignmentServices(IPartyLeaderAssignmentRepository repository, IMapper mapper,
        IUserRepository userRepository, IPoliticalPartyRepository politicalPartyRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _politicalPartyRepository = politicalPartyRepository;
        _partyLeaderAssignmentRepository = repository;
    }

    public override async Task<Result<List<PartyLeaderAssignmentDto>>> GetAllAsync()
    {
        var partyLeaders = await _partyLeaderAssignmentRepository.GetAllQueryable()
            .Include(pt => pt.User)
            .Include(pt => pt.PoliticalParty)
            .Select(pt => new PartyLeaderAssignmentDto()
            {
                Id = pt.Id,
                PoliticalPartyId = pt.PoliticalPartyId,
                UserId = pt.UserId,
                User = _mapper.Map<UserDto>(pt.User),
                PoliticalParty = _mapper.Map<PoliticalPartyDto>(pt.PoliticalParty)
            }).ToListAsync();
        return Result<List<PartyLeaderAssignmentDto>>.Ok(partyLeaders);

        //return base.GetAllAsync(); Perdoname principo de sustitución de liskov
    }

    public override async Task<Result<PartyLeaderAssignmentDto>> AddAsync(PartyLeaderAssignmentDto dtoModel)
    {
        if (await _partyLeaderAssignmentRepository.ThisLeaderExists(dtoModel.UserId))
        {
            return Result<PartyLeaderAssignmentDto>.Fail(
                "Este dirigente politico ya esta asociado a otro partido",
                fieldName: nameof(dtoModel.UserId),
                MessageType.Field
            );
        }

        return await base.AddAsync(dtoModel);
    }

    public async Task<Result<List<UserDto>>> GetAllAvailableAndActivePartyLeaders()
    {
        var users = await _userRepository
            .GetAllQueryable()
            .AsNoTracking()
            .Where(u => u.Role == UserRole.PoliticalLeader && u.IsActive)
            .ToListAsync();

        var partyLeadersAssociated = await _partyLeaderAssignmentRepository
            .GetAllQueryable()
            .AsNoTracking()
            .Select(pl => pl.UserId)
            .ToListAsync();

        users.RemoveAll(u => partyLeadersAssociated.Contains(u.Id));

        return Result<List<UserDto>>.Ok(_mapper.Map<List<UserDto>>(users));
    }

    public async Task<Result<PoliticalPartyDto>> GetPoliticalPartyAsocciated(int userId)
    {
        var politicalLeader = await _partyLeaderAssignmentRepository.GetAllQueryable()
            .FirstOrDefaultAsync(p => p.UserId == userId);
        
        var politicalPartyId = await _politicalPartyRepository.GetByIdAsync(politicalLeader!.PoliticalPartyId);

        var dto = _mapper.Map<PoliticalPartyDto>(politicalPartyId);
        return Result<PoliticalPartyDto>.Ok(dto);
    }
}