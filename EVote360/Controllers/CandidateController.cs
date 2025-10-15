using AutoMapper;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Candidate;
using Evote360.Core.Application.ViewModels.User;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class CandidateController : Controller
{
    private readonly IMapper _mapper;
    private readonly ICandidateServices _candidateServices;
    private readonly IUserSession _userSession;
    private readonly IPartyLeaderAssignmentServices _partyLeaderAssignmentServices;

    public CandidateController(ICandidateServices politicalPartyServices, IMapper mapper, IUserSession userServices,
        IPartyLeaderAssignmentServices partyLeaderAssignmentServices)
    {
        _candidateServices = politicalPartyServices;
        _mapper = mapper;
        _userSession = userServices;
        _partyLeaderAssignmentServices = partyLeaderAssignmentServices;
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
            return View(new List<CandidateViewModel>());
        }

        var candidatesResult =
            await _candidateServices.GetAllTheCandidatesOfThisPoliticalPartyAsync(politicalPartyResult.Value!.Id);
        if (candidatesResult.IsFailure)
        {
            MessageHandler.Handle(candidatesResult, this);
            return View(new List<CandidateViewModel>());
        }

        var models = _mapper.Map<List<CandidateViewModel>>(candidatesResult.Value);
        return View(models);
    }

    public async Task<IActionResult> Create()
    {
        UserViewModel? userSession = _userSession.GetUserSession();
        if (userSession == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        var createViewModel = new CandidateCreateViewModel()
        {
            Id = 0,
            FirstName = "",
            LastName = "",
            PhotoFile = null,
            PoliticalPartyId = 0,
            IsActive = true
        };
        
        var politicalPartyResult = await _partyLeaderAssignmentServices.GetPoliticalPartyAsocciated(userSession.Id);

        if (politicalPartyResult.IsFailure)
        {
            MessageHandler.Handle(politicalPartyResult, this);
            return View(createViewModel);
        }
        createViewModel.PoliticalPartyId = politicalPartyResult.Value!.Id;
        
        return View(createViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CandidateCreateViewModel createViewModel)
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

        var dto = _mapper.Map<CandidateDto>(createViewModel);
        var createResult = await _candidateServices.AddAsync(dto);
        
        if (createResult.IsFailure)
        {
            MessageHandler.Handle(createResult, this);
            return View(createViewModel);
        }
        
        var dtoWithPhotoPath = _mapper.Map<CandidateDto>(createResult.Value);
        dtoWithPhotoPath.PhotoPath = FileManager.Upload(createViewModel.PhotoFile, dtoWithPhotoPath.Id, "candidates");

        var updateResult = await _candidateServices.UpdateAsync(dtoWithPhotoPath.Id, dtoWithPhotoPath);

        if (updateResult.IsFailure)
        {
            MessageHandler.Handle(createResult, this);
            return View(createViewModel);
        }

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        var result = await _candidateServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<CandidateUpdateViewModel>(value);
            return View(viewModel);
        }

        // Considerar poner un Alert en la vista con un Failure
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CandidateUpdateViewModel updateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(updateViewModel);
        }

        var result = await _candidateServices.GetByIdAsync(updateViewModel.Id);

        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(updateViewModel);
        }

        var dto = _mapper.Map<CandidateDto>(updateViewModel);
        dto.PhotoPath = FileManager.Upload(updateViewModel.PhotoFile, dto.Id, "politicalParties", true,
            result.Value!.PhotoPath);
        var updateResult = await _candidateServices.UpdateAsync(dto.Id, dto);

        if (updateResult.IsFailure)
        {
            MessageHandler.Handle(updateResult, this);
            return View(updateViewModel);
        }

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> ChangeState(int id)
    {
        var result = await _candidateServices.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            var value = result.Value;
            var viewModel = _mapper.Map<CandidateChangeStateViewModel>(value);
            return View(viewModel);
        }

        // Considerar poner un Alert en la vista con un Failure
        return View("ChangeState");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeState(CandidateChangeStateViewModel model)
    {
        await _candidateServices.SetActiveAsync(model.Id, !model.IsActive);
        return RedirectToAction(nameof(Index));
    }
}