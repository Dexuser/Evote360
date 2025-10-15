using AutoMapper;
using Evote360.Core.Application;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Application.Helpers;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.User;
using Evote360.Core.Domain.Common.Enums;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class LoginController : Controller
{
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;

    public LoginController(IUserServices userServices, IMapper mapper)
    {
        _userServices = userServices;
        _mapper = mapper;
    }

    // GET
    public IActionResult Index()
    {
        // Tiene que recuperar al usuario y redireccionar a un Layout especifico segun su rol
        UserViewModel? userSession = HttpContext.Session.Get<UserViewModel>("user");
        if (userSession != null)
        {
            return RedirectToHomeByRole(userSession);
        }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel viewModel)
    {
        UserViewModel? userSession = HttpContext.Session.Get<UserViewModel>("user");
        if (userSession != null)
        {
            return RedirectToHomeByRole(userSession);
        }
        
        if (!ModelState.IsValid)
        {
            viewModel.Password = "";
            return View(viewModel);
        }

        Result<UserDto> result = await _userServices.LoginAsync(
            new LoginDto()
        {
            Username = viewModel.UserName,
            Password = viewModel.Password
        });
        
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            viewModel.Password = "";
            return View(viewModel);
        }
        
        UserDto? userDto = result.Value;
        UserViewModel userViewModel = _mapper.Map<UserViewModel>(userDto);
        HttpContext.Session.Set("user",  userViewModel);
        
        return RedirectToHomeByRole(userViewModel);
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("user");
        return RedirectToRoute(new { controller = "Login", action = "Index" });
    }
    

    public IActionResult AccessDenied()
    {
        return View();
    }
    
    public void SendAlertMessage(string? message = "")
    {
        ViewBag.Message = message;
    }

    public IActionResult RedirectToHomeByRole(UserViewModel userViewModel)
    {
        switch (userViewModel.Role)
        {
            case (int)UserRole.Administrator:
                return RedirectToRoute(new {controller = "AdminHome", action = "Index"});
            case (int)UserRole.PoliticalLeader:
                return RedirectToRoute(new {controller = "PartyLeaderHome", action = "Index"});
            default:
                return RedirectToRoute(new {controller = "Login", action = "Index"});
        }
    }
        
        
}