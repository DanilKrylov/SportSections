using Microsoft.AspNetCore.Mvc;


namespace SportSections.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "SportSections");
        }
    }
}
