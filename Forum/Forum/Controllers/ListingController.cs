using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renting.ViewModels;
using Renting.Models;

namespace Renting.Controllers
{
    public class ListingController : Controller
    {
        private readonly ListingDbContext _listingDbContext;

        public ListingController(ListingDbContext listingDbContext)
        {
            _listingDbContext = listingDbContext;
        }

        public IActionResult Table()
        {
            List<Listing> listings = _listingDbContext.Listings.ToList();
            var listingListViewModel = new ListingListViewModel(listings, "Table");
            return View(listingListViewModel);
        }

        public IActionResult Grid()
        {
            List<Listing> listings = _listingDbContext.Listings.ToList();
            var listingListViewModel = new ListingListViewModel(listings, "Grid");
            return View(listingListViewModel);
        }

        public IActionResult Details(int id)
        {
            List<Listing> listings = _listingDbContext.Listings.ToList();
            var listing = listings.FirstOrDefault(i => i.ListingId == id);
            if (listing == null)
                return NotFound();
            return View(listing);
        }
    }
}