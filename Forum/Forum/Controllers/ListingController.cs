using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Forum.ViewModels;

namespace Forum.Controllers
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
        // Fjerne Grid/Table View, Endre til ett view, hvor vi stacker listing bokser oppå hverandre, ikke i et table, men table kan brukes
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _listingDbContext.Listings.Add(listing);
                _listingDbContext.SaveChanges();
                return RedirectToAction(nameof(Table));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var listing = _listingDbContext.Listings.Find(id);
            if(listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }
        [HttpPost]
        public IActionResult Update(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _listingDbContext.Listings.Update(listing);
                _listingDbContext.SaveChanges();
                return RedirectToAction(nameof(Table));
            }
            return View(listing);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var listing = _listingDbContext.Listings.Find(id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var listing = _listingDbContext.Listings.Find(id);
            if (listing == null)
            {
                return NotFound();
            }
            _listingDbContext.Listings.Remove(listing);
            _listingDbContext.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
    }
}