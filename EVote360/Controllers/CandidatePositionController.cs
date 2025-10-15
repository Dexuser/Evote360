using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.CandidatePosition;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Candidate;
using Evote360.Core.Application.ViewModels.CandidatePosition;
using Evote360.Core.Application.ViewModels.User;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVote360.Controllers;

public class CandidatePositionController : Controller
{
    private readonly IMapper _mapper;
    private readonly ICandidatePositionServices _candidatePositionServices;
    private readonly ICandidateServices _candidateServices;
    private readonly IUserSession _userSession;
    private readonly IPartyLeaderAssignmentServices _partyLeaderAssignmentServices;

    public CandidatePositionController(IMapper mapper, ICandidatePositionServices candidatePositionServices,
        IUserSession userSession, IPartyLeaderAssignmentServices partyLeaderAssignmentServices, ICandidateServices candidateServices)
    {
        _mapper = mapper;
        _candidatePositionServices = candidatePositionServices;
        _userSession = userSession;
        _partyLeaderAssignmentServices = partyLeaderAssignmentServices;
        _candidateServices = candidateServices;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        
        var politicalPartyResult =
            await _partyLeaderAssignmentServices.GetPoliticalPartyAsocciated(userSession.Id);
        
        if (politicalPartyResult.IsFailure)
        {
            MessageHandler.Handle(politicalPartyResult, this);
            return View(new List<CandidatePositionViewModel>());
        }

        var candidatesResult =
            await _candidatePositionServices.GetAllTheCandidatesAssignedByThisPartyAsync(politicalPartyResult.Value!.Id);
        if (candidatesResult.IsFailure)
        {
            MessageHandler.Handle(candidatesResult, this);
            return View(new List<CandidatePositionViewModel>());
        }

        var models = _mapper.Map<List<CandidatePositionViewModel>>(candidatesResult.Value);
        return View(models);
    }

    public async Task<IActionResult> Create()
    {
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        
        var politicalPartyResult =
            await _partyLeaderAssignmentServices.GetPoliticalPartyAsocciated(userSession.Id);
        
        var createViewModel = new CandidatePositionCreateViewModel()
        {
            Id = 0,
            CandidateId = 0,
            ElectivePositionId = 0,
            PoliticalPartyId = 0,
        };       

        
        if (politicalPartyResult.IsFailure)
        {
            MessageHandler.Handle(politicalPartyResult, this);
            return View(createViewModel);
        }
        await PopulateTheCreateViewBags(politicalPartyResult.Value!.Id);
        createViewModel.PoliticalPartyId = politicalPartyResult.Value!.Id;
        
        return View(createViewModel);
   }

    public async Task PopulateTheCreateViewBags(int politicalPartyId)
    {
        var candidatesResult =
            await _candidateServices.GetAllTheCandidatesAndAllyCandidatesOfThisPoliticalPartyAsync(politicalPartyId);
        
        var positionsResult =
            await _candidatePositionServices.GetAllTheAvailableElectivePositionsForThisPartyAsync(politicalPartyId);
        
        
        if (candidatesResult.IsSuccess && positionsResult.IsSuccess)
        {
            List<SelectListItem> candidates = new List<SelectListItem>();
            foreach (var candidate in candidatesResult.Value!)
            {
                candidates.Add(new SelectListItem($"{candidate.FirstName} {candidate.LastName} {candidate.PoliticalPartyId}", $"{candidate.Id}"));
            }
            
            ViewBag.Candidates = candidates;
            ViewBag.ElectivePositions = new SelectList(positionsResult.Value, "Id", "Name");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> Create(CandidatePositionCreateViewModel createViewModel)
    {
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        if (!ModelState.IsValid)
        {
            return View(createViewModel);
        }

        var dto = _mapper.Map<CandidatePositionDto>(createViewModel);
        var createResult = await _candidatePositionServices.AddAsync(dto);
        
        if (createResult.IsFailure)
        {
            MessageHandler.Handle(createResult, this);
            return View(createViewModel);
        }

        return RedirectToAction(nameof(Index));
    }


    public  IActionResult Delete(int id)
    {
        return View(new CandidatePositionDeleteViewModel { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(CandidatePositionDeleteViewModel model)
    {
        await _candidatePositionServices.DeleteAsync(model.Id);
        return RedirectToAction(nameof(Index));
    }
}