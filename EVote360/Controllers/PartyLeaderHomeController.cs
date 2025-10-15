using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class PartyLeaderHomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}