using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using System.Reflection;

namespace Forum.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ListingsDbContext _listingsDbContext;

        public ListingsController(ListingsDbContext listingsDbContext)
        {
            _listingsDbContext = listingsDbContext;
        }

        public IActionResult Table()
        {
            var listings = new List<Listings>();

            var listing1 = new Listings
            {
                ListingId = 1,
                Name = "Name",
                Price = 1000
            };

            var listing2 = new Listings
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