using Microsoft.AspNetCore.Mvc;

namespace NZWalk.API.Controllers
{
    public class WalkDifficultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
