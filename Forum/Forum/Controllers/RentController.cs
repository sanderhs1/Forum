using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class RentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
