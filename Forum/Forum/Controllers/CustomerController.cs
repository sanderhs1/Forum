using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers 
{
    
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
