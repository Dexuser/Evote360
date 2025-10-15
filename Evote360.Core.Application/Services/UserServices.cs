using AutoMapper;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Services;

public class UserServices : GenericServices<User, UserDto>, IUserServices
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;
    private readonly IPartyLeaderAssignmentRepository _partyLeaderAssignmentRepository;

    public UserServices(IUserRepository userRepository, IMapper mapper,
        IPartyLeaderAssignmentRepository partyLeaderAssignmentRepository) : base(userRepository, mapper)
    {
        _mapper = mapper;
        _partyLeaderAssignmentRepository = partyLeaderAssignmentRepository;
        _repository = userRepository;
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        await _repository.SetActiveAsync(id, isActive);
    }

    public override async Task<Result<UserDto>> AddAsync(UserDto dtoModel)
    {
        if (await _repository.ThisUserNameExitsAsync(dtoModel.Username, dtoModel.Id))
        {
            return Result<UserDto>.Fail("Este nombre de usuario ya esta en uso",
                fieldName: nameof(dtoModel.Username),
                messageType: MessageType.Field);
        }

        dtoModel.Password = PasswordEncryptation.ComputeSha256Hash(dtoModel.Password);
        return await base.AddAsync(dtoModel);
    }

    public override async Task<Result<UserDto>> UpdateAsync(int id, UserDto dtoModel)
    {
        if (await _repository.ThisUserNameExitsAsync(dtoModel.Username, dtoModel.Id))
        {
            return Result<UserDto>.Fail("Este nombre de usuario ya esta en uso",
                fieldName: nameof(dtoModel.Username),
                messageType: MessageType.Field);
        }

        var existingUser = await _repository.GetByIdAsync(id);
        if (existingUser == null)
        {
            return Result<UserDto>.Fail("Este usuario no existe", messageType: MessageType.Alert);
        }

        if (String.IsNullOrEmpty(dtoModel.Password))
        {
            dtoModel.Password = existingUser.PasswordHash;
        }
        else
        {
            dtoModel.Password = PasswordEncryptation.ComputeSha256Hash(dtoModel.Password);
        }

        return await base.UpdateAsync(id, dtoModel);
    }

    public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            string passwordHash = PasswordEncryptation.ComputeSha256Hash(loginDto.Password);
            User? user = await _repository.Login(loginDto.Username, passwordHash);
            if (user == null)
            {
                return Result<UserDto>.Fail("Credenciales invalidas", messageType: MessageType.Summary);
            }

            if (!user.IsActive)
            {
                return Result<UserDto>.Fail("Este usuario no esta activo", messageType: MessageType.Alert);
            }

            if (user.Role == UserRole.PoliticalLeader && !await _partyLeaderAssignmentRepository.ThisLeaderExists(user.Id))
            {
                return Result<UserDto>.Fail(
                    error:"No tiene un partido político asignado, por lo tanto no puede iniciar sesión. " +
                          "Por favor, póngase en contacto con un administrador.",
                    messageType: MessageType.Alert);
            }

            return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
        catch (Exception e)
        {
            return Result<UserDto>.Fail(e.Message);
        }
    }
    
    public async Task<List<UserDto>> GetAllTActiveAsync()
    {
        var entities = await _repository.GetAllTActiveAsync();
        return _mapper.Map<List<UserDto>>(entities);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _repository.IsActiveAsync(id);
    }
}