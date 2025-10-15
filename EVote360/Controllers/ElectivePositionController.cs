using AutoMapper;
using Evote360.Core.Application.Dtos;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class ElectivePositionController : Controller
{
    private readonly IMapper _mapper;
    private readonly IElectivePositionServices _electivePositionServices;

    public ElectivePositionController(IElectivePositionServices electivePositionServices, IMapper mapper)
    {
        _electivePositionServices = electivePositionServices;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _electivePositionServices.GetAllAsync();
        if (result.IsFailure)
        {
            SendAlertMessage(result.Error!);
            return View(new List<ElectivePositionViewModel>());
        }
        var models = _mapper.Map<List<ElectivePositionViewModel>>(result.Value);
        return View(models);
    }

    public IActionResult Save()
    {
        return View(new ElectivePositionSaveViewModel()
        {
            Id = 0,
            Name = "",
            Description = "",
            IsActive = true 
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(ElectivePositionSaveViewModel saveViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(saveViewModel);
        }

        var dto = _mapper.Map<ElectivePositionDto>(saveViewModel);
        var result = await _electivePositionServices.AddAsync(dto);
        if (result.IsFailure)
        {
            SendAlertMessage(result.Error!);
            return View(saveViewModel);
        }

        return RedirectToAction(nameof(Index));
    }
    
     
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.EditMode = true;
        var result = await _electivePositionServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            ElectivePositionDto? value = result.Value;
            ElectivePositionSaveViewModel viewModel = _mapper.Map<ElectivePositionSaveViewModel>(value);
            return View("Save", viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("Save");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ElectivePositionSaveViewModel saveViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Save" ,saveViewModel);
        }
        var dto = _mapper.Map<ElectivePositionDto>(saveViewModel);
        var result = await _electivePositionServices.UpdateAsync(dto.Id,dto);
        
        if (result.IsFailure)
        {
            SendAlertMessage(result.Error!);
            return View("Save",  saveViewModel);
        }
        
        return RedirectToAction(nameof(Index));
    }

    
    public async Task<IActionResult> ChangeState(int id)
    {
        var result = await _electivePositionServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            ElectivePositionDto? value = result.Value;
            var viewModel = _mapper.Map<ElectivePositionChangeStateViewModel>(value);
            return View("ChangeState", viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("ChangeState");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeState(ElectivePositionChangeStateViewModel model)
    {
        await _electivePositionServices.SetActiveAsync(model.Id, !model.IsActive);
        return RedirectToAction(nameof(Index));
    }

    private void SendAlertMessage(string? message = null)
    {
        ViewBag.Message = message;
    }
    
}