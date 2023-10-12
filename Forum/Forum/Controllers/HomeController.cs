using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers 
{
    //This controller class called HomeController will handle requests and responses 
    public class HomeController : Controller
    {
        //When users visit the site´s home page this this method will return the content to be displayed in the user´s web browser
        public IActionResult Index()
        {
            return View();
        }
    }
}
