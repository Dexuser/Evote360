using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using Evote360.Core.Application.ViewModels.PoliticalParty;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class PoliticalPartyController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPoliticalPartyServices _politicalPartyServices;

    public PoliticalPartyController(IPoliticalPartyServices politicalPartyServices, IMapper mapper)
    {
        _politicalPartyServices = politicalPartyServices;
        _mapper = mapper;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _politicalPartyServices.GetAllAsync();
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(new List<PoliticalPartyViewModel>());
        }
        var models = _mapper.Map<List<PoliticalPartyViewModel>>(result.Value);
        return View(models);
    }

    public IActionResult Create()
    {
        return View(new PoliticalPartyCreateViewModel()
        {
            Id = 0,
            Name = "",
            Acronym = "",
            Description = "",
            LogoFile = null,
            IsActive = true 
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(PoliticalPartyCreateViewModel createViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createViewModel);
        }

        var dto = _mapper.Map<PoliticalPartyDto>(createViewModel);
        dto.Acronym = dto.Acronym.ToUpper();
        var result = await _politicalPartyServices.AddAsync(dto);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(createViewModel);
        }
        var dtoWithLogoPath =  _mapper.Map<PoliticalPartyDto>(result.Value);
        dtoWithLogoPath.LogoPath = FileManager.Upload(createViewModel.LogoFile, dtoWithLogoPath.Id,"politicalParties");
        
        var updateResult = await _politicalPartyServices.UpdateAsync(dtoWithLogoPath.Id,dtoWithLogoPath);
        
        if (updateResult.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(createViewModel);
        }
        

        return RedirectToAction(nameof(Index));
    }
    
     
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _politicalPartyServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<PoliticalPartyUpdateViewModel>(value);
            return View(viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(PoliticalPartyUpdateViewModel updateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(updateViewModel);
        }

        var result = await _politicalPartyServices.GetByIdAsync(updateViewModel.Id);
        
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(updateViewModel);
        }
        
        var dto = _mapper.Map<PoliticalPartyDto>(updateViewModel);
        dto.Acronym = dto.Acronym.ToUpper();
        dto.LogoPath = FileManager.Upload(updateViewModel.LogoFile, dto.Id, "politicalParties", true, result.Value!.LogoPath);
        var updateResult = await _politicalPartyServices.UpdateAsync(dto.Id,dto);
        
        if (updateResult.IsFailure)
        {
            MessageHandler.Handle(updateResult, this);
            return View(updateViewModel);
        }
        return RedirectToAction(nameof(Index));
    }

    
    public async Task<IActionResult> ChangeState(int id)
    {
        var result = await _politicalPartyServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<PoliticalPartyChangeStateViewModel>(value);
            return View (viewModel);
        }
        // Considerar poner un Alert en la vista con un Failure
        return View("ChangeState");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeState(ElectivePositionChangeStateViewModel model)
    {
        await _politicalPartyServices.SetActiveAsync(model.Id, !model.IsActive);
        return RedirectToAction(nameof(Index));
    }
   
}