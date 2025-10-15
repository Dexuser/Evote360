using AutoMapper;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using Evote360.Core.Application.ViewModels.User;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices, IMapper mapper)
    {
        _userServices = userServices;
        _mapper = mapper;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _userServices.GetAllAsync();
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(new List<UserViewModel>());
        }
        var models = _mapper.Map<List<UserViewModel>>(result.Value);
        return View(models);
    }

    public IActionResult Create()
    {
        return View(new UserCreateViewModel()
        {
            Id = 0,
            FirstName = "",
            LastName = "",
            Email = "",
            UserName = "",
            Password = "",
            ConfirmPassword = "",
            Role = 0,
            IsActive = true 
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateViewModel createViewModel)
    {
        
        if (!ModelState.IsValid)
        {
            return View(createViewModel);
        }

        var dto = _mapper.Map<UserDto>(createViewModel);
        var result = await _userServices.AddAsync(dto);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(createViewModel);
        }

        return RedirectToAction(nameof(Index));
    }
    
     
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _userServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<UserUpdateViewModel>(value);
            return View(viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(UserUpdateViewModel updateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(updateViewModel);
        }
        
        var dto = _mapper.Map<UserDto>(updateViewModel);
        var updateResult = await _userServices.UpdateAsync(dto.Id,dto);
        
        if (updateResult.IsFailure)
        {
            MessageHandler.Handle(updateResult, this);
            return View(updateViewModel);
        }
        return RedirectToAction(nameof(Index));
    }

    
    public async Task<IActionResult> ChangeState(int id)
    {
        var result = await _userServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<UserChangeStateViewModel>(value);
            return View (viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("ChangeState");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeState(ElectivePositionChangeStateViewModel model)
    {
        await _userServices.SetActiveAsync(model.Id, !model.IsActive);
        return RedirectToAction(nameof(Index));
    }
   
}