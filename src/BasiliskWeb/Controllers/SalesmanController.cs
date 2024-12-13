using BasiliskWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles = "Admin")]
public class SalesmanController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SalesmanRegion()
    {
        return View("SalesmanRegion");
    }
}