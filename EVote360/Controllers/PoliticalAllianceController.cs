using AutoMapper;
using Evote360.Core.Application.Dtos.PoliticalAlliance;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.PoliticalAlliance;
using Evote360.Core.Application.ViewModels.User;
using Evote360.Core.Domain.Common.Enums;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVote360.Controllers;

public class PoliticalAllianceController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPoliticalAllianceServices _politicalAllianceServices;
    private readonly IPartyLeaderAssignmentServices _partyLeaderAssignmentServices;
    private readonly IUserSession _userSession;

    public PoliticalAllianceController(IPoliticalAllianceServices politicalAllianceServices,
        IPartyLeaderAssignmentServices partyLeaderAssignmentServices, IUserSession userSession, IMapper mapper)
    {
        _politicalAllianceServices = politicalAllianceServices;
        _partyLeaderAssignmentServices = partyLeaderAssignmentServices;
        _userSession = userSession;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        
        var partyResult =
            await _partyLeaderAssignmentServices.GetPoliticalPartyAsocciated(userSession.Id);       
        
        if (partyResult.IsFailure)
        {
            MessageHandler.Handle(partyResult, this);
            return View();
        }       
        
        var allianceResult = 
            await _politicalAllianceServices.GetAllTheRequestsOfThisPoliticalParty(partyResult.Value!.Id);
        
        if (allianceResult.IsFailure)
        {
            MessageHandler.Handle(partyResult, this);
            return View();
        }


        var alliancesViewModel = _mapper.Map<List<PoliticalAllianceViewModel>>(allianceResult.Value);
        var indexViewModel = new PoliticalAllianceIndexViewModel();
        
        indexViewModel.RequestsAlliances =
            alliancesViewModel.Where(al => 
                al.TargetPartyId == partyResult.Value!.Id && 
                al.Status != AllianceStatus.Accepted).ToList();
        
        indexViewModel.TargetAlliances =
            alliancesViewModel.Where(al => 
                al.RequestingPartyId == partyResult.Value!.Id &&
                al.Status == AllianceStatus.Pending).ToList();
        
        indexViewModel.ActualAlliances = alliancesViewModel.Where(al => al.Status == AllianceStatus.Accepted).ToList();
        
        
        return View(indexViewModel);
    }


    public async Task<IActionResult> ChangeState(int politicalAllianceId, bool accepted, string politicalPartyName,
        string politicalPartyAcronym)
    {
        return View(new PoliticalAllianceAcceptRequestViewModel()
        {
            PoliticalAllianceRequestId = politicalAllianceId,
            Accepted = accepted,
            Name =  politicalPartyName,
            Acronym = politicalPartyAcronym
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> ChangeState(PoliticalAllianceAcceptRequestViewModel model)
    {
        var result = await _politicalAllianceServices.AcceptThisAllianceRequest(model.PoliticalAllianceRequestId, model.Accepted);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
        }
        return RedirectToRoute(new { controller = "PoliticalAlliance", action = "Index" });
    }

    public async Task<IActionResult> Create()
    {
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        
        var partyResult =
            await _partyLeaderAssignmentServices.GetPoliticalPartyAsocciated(userSession.Id);
        
        var availablePartyResult =
            await _politicalAllianceServices.GetAllTheAvailablePoliticalParties(partyResult.Value!.Id);
    
        ViewBag.PoliticalParties = new SelectList(availablePartyResult.Value, "Id", "Name");

        return View(new PoliticalAllianceCreateViewModel()
        {
            Id = 0,
            RequestingPartyId = partyResult.Value!.Id,
            TargetPartyId = 0
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(PoliticalAllianceCreateViewModel createViewModel)
    {
        var dto = _mapper.Map<PoliticalAllianceDto>(createViewModel);
        var result = await _politicalAllianceServices.AddAsync(dto);
        
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(createViewModel);
        }
        return RedirectToRoute(new { controller = "PoliticalAlliance", action = "Index" });
    }
    
    public IActionResult Delete(int id)
    {
        return View(new PoliticalAllianceDeleteViewModel() {Id = id});
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(PoliticalAllianceDeleteViewModel deleteViewModel)
    {
        var result = await _politicalAllianceServices.DeleteAsync(deleteViewModel.Id);
        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(deleteViewModel);
        }

        return RedirectToRoute(new { controller = "PoliticalAlliance", action = "Index" });
    }
}