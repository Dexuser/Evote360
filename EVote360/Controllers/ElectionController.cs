using AutoMapper;
using Evote360.Core.Application.Dtos.Election;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Election;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Interfaces;
using EVote360.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class ElectionController : Controller
{
    private readonly IElectionServices _electionServices;
    private readonly IMapper _mapper;

    public ElectionController(IElectionServices electionServices, IMapper mapper)
    {
        _electionServices = electionServices;
        _mapper = mapper;
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var result = await _electionServices.GetSummaryOfAllElections();
        return View(_mapper.Map<List<ElectionSummaryViewModel>>(result.Value));
    }
    
    public IActionResult Create()
    {
        return View(new ElectionCreateViewModel()
        {
            Id = 0,
            Name = "",
            Date = DateTime.Now 
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ElectionCreateViewModel createViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createViewModel);
        }
        
        var dto = _mapper.Map<ElectionDto>(createViewModel);
        dto.Status = ElectionStatus.InProgress;
        var result = await _electionServices.AddAsync(dto);

        if (result.IsFailure)
        {
            MessageHandler.Handle(result, this);
            return View(createViewModel);
        }
        
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details()
    {
        return View();
    }
    public IActionResult FinalizeElection()
    {
        return View();
    }
    
}