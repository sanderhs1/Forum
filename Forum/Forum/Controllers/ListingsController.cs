using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class ListingsController : Controller
    {
        public IActionResult Table()
        {
            var listings = new List<Listing>();

            var listing1 = new Listing
            {
                ListingId = 1,
                Name = "Name",
                Price = 1000
            };

            var listing2 = new Listing
            {
                ListingId = 2,
                Name = "Name",
                Price = 2000
            };

            listings.Add(listing1);
            listings.Add(listing2);

            ViewBag.CurrentViewName = "Listings";
            return View(listings);
        }
    }
}