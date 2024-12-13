using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index() {
        return View("Index");
    }
}