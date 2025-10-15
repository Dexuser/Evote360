using AutoMapper;
using Evote360.Core.Application.Interfaces;
using Evote360.Core.Application.ViewModels.Election;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVote360.Controllers;

public class AdminHomeController : Controller
{
    private readonly IElectionServices _electionServices;
    private readonly IMapper _mapper;

    public AdminHomeController(IElectionServices electionServices, IMapper mapper)
    {
        _electionServices = electionServices;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        await PopulateIndexViewBag();
        return View(new List<ElectionSummaryCandidateViewModel>());
    }
    
    [HttpPost]
    public async Task<IActionResult> GetTheSummaryWithCandidates(int year)
    {
        var result = 
            await _electionServices.GetSummaryOfElectionsWithCandidates(year);
        await PopulateIndexViewBag();
        
        var vm = _mapper.Map<List<ElectionSummaryCandidateViewModel>>(result.Value);
        return View("Index", vm);
    }

    public async Task PopulateIndexViewBag()
    {
        var result = await _electionServices.GetYearsWithElections();
        if (result.IsSuccess)
        {
            ViewBag.Years = new SelectList(result.Value);
        }
    }
}