using AutoMapper;
using Evote360.Core.Application;
using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Citizen;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class CitizenController : Controller
{
    private readonly IMapper _mapper;
    private readonly ICitizenServices _citizenServices;

    public CitizenController(ICitizenServices citizenServices, IMapper mapper)
    {
        _citizenServices = citizenServices;
        _mapper = mapper;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _citizenServices.GetAllAsync();
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(new List<CitizenViewModel>());
        }
        var models = _mapper.Map<List<CitizenViewModel>>(result.Value);
        return View(models);
    }

    public IActionResult Save()
    {
        return View(new CitizenSaveViewModel()
        {
            Id = 0,
            FirstName = "",
            LastName = "",
            Email = "",
            IdentityNumber = "",
            IsActive = true 
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(CitizenSaveViewModel saveViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(saveViewModel);
        }

        var dto = _mapper.Map<CitizenDto>(saveViewModel);
        var result = await _citizenServices.AddAsync(dto);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(saveViewModel);
        }

        return RedirectToAction(nameof(Index));
    }
    
     
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.EditMode = true;
        var result = await _citizenServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            CitizenDto? value = result.Value;
            var viewModel = _mapper.Map<CitizenSaveViewModel>(value);
            return View("Save", viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("Save");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CitizenSaveViewModel saveViewModel)
    {
        ViewBag.EditMode = true;
        if (!ModelState.IsValid)
        {
            return View("Save" ,saveViewModel);
        }
        var dto = _mapper.Map<CitizenDto>(saveViewModel);
        var result = await _citizenServices.UpdateAsync(dto.Id,dto);
        
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View("Save",  saveViewModel);
        }
        
        return RedirectToAction(nameof(Index));
    }

    
    public async Task<IActionResult> ChangeState(int id)
    {
        var result = await _citizenServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<CitizenChangeStateViewModel>(value);
            return View("ChangeState", viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("ChangeState");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeState(ElectivePositionChangeStateViewModel model)
    {
        await _citizenServices.SetActiveAsync(model.Id, !model.IsActive);
        return RedirectToAction(nameof(Index));
    }
   
}