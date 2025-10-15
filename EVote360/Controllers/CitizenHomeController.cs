using Microsoft.AspNetCore.Mvc;

namespace EVote360.Controllers;

public class CitizenHomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}