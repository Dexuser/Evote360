using AutoMapper;
using Evote360.Core.Application;
using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Dtos.PartyLeaderAssignment;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Citizen;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using Evote360.Core.Application.ViewModels.PartyLeaderAssignment;
using Evote360.Core.Domain.Entities;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVote360.Controllers;

public class PartyLeaderAssignmentController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPartyLeaderAssignmentServices _partyLeaderAssignmentService;
    private readonly IPoliticalPartyServices _politicalPartyService;

    public PartyLeaderAssignmentController(IPartyLeaderAssignmentServices citizenServices, IMapper mapper, IPoliticalPartyServices politicalPartyService)
    {
        _partyLeaderAssignmentService = citizenServices;
        _mapper = mapper;
        _politicalPartyService = politicalPartyService;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _partyLeaderAssignmentService.GetAllAsync();
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(new List<PartyLeaderAssignmentViewModel>());
        }
        var models = _mapper.Map<List<PartyLeaderAssignmentViewModel>>(result.Value);
        return View(models);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateViewBag();
        return View(new PartyLeaderAssignmentCreateViewModel()
        {
            Id = 0,
            PoliticalPartyId = 0,
            UserId = 0,
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(PartyLeaderAssignmentCreateViewModel saveViewModel)
    {
        
        await PopulateViewBag();
        if (!ModelState.IsValid)
        {
            return View(saveViewModel);
        }

        var dto = _mapper.Map<PartyLeaderAssignmentDto>(saveViewModel);
        var result = await _partyLeaderAssignmentService.AddAsync(dto);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(saveViewModel);
        }

        return RedirectToAction(nameof(Index));
    }


    public  IActionResult Delete(int id)
    {
        return View(new PartyLeaderAssignmenDeleteViewModel(){Id = id});
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(PartyLeaderAssignmenDeleteViewModel deleteViewModel)
    {
        await _partyLeaderAssignmentService.DeleteAsync(deleteViewModel.Id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateViewBag()
    {
        var leadersResult = await _partyLeaderAssignmentService.GetAllAvailableAndActivePartyLeaders();
        var politicalPartyResult = await _politicalPartyService.GetAllActivePoliticalParties();
        
        if (leadersResult.IsSuccess && politicalPartyResult.IsSuccess)
        {
            List<SelectListItem> partyLeadersAvailable = new List<SelectListItem>();
            foreach (var user in leadersResult.Value!)
            {
                partyLeadersAvailable.Add(new SelectListItem($"{user.FirstName} {user.LastName}", $"{user.Id}"));
            }
            
            ViewBag.PoliticalLeaders = partyLeadersAvailable;
            ViewBag.PoliticalParties = new SelectList(politicalPartyResult.Value, "Id", "Acronym");
        }
        
    }
}