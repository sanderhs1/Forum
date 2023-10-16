using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Forum.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class ListingController : Controller
    {
        private readonly ListingDbContext _listingDbContext;
        private readonly ILogger<ListingController> _logger;

        public ListingController(ListingDbContext listingDbContext, ILogger<ListingController> logger)
        {
            _logger = logger;
            _listingDbContext = listingDbContext;
        }

        public List<Rent> RentConsole()
        {
            return _listingDbContext.Rents.ToList();

        }
        

        public async Task<IActionResult>Table()
        {
            _logger.LogInformation("This is an information message");
            _logger.LogWarning("This is a warning message");
            _logger.LogError("This is an error message");
            List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
            var listingListViewModel = new ListingListViewModel(listings, "Table");
            return View(listingListViewModel);
        }
        // Fjerne Grid/Table View, Endre til ett view, hvor vi stacker listing bokser oppå hverandre, ikke i et table, men table kan brukes
        public async Task<IActionResult> Grid()
        {
            
            List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
            if (listings == null)
            {
                _logger.LogError("[ListingController] Listing is not found");
                return NotFound("Listings not found");
            }
            var listingListViewModel = new ListingListViewModel(listings, "Grid");
            return View(listingListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var listing = await _listingDbContext.Listings.FirstOrDefaultAsync(i => i.ListingId == id);
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
        public async Task<IActionResult>Create(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _listingDbContext.Listings.Add(listing);
                await _listingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
               // Feil her _logger.LogWarning("[ListingController] Listings creation failed {@listing}", listing);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
            var listing = await _listingDbContext.Listings.FindAsync(id);
            if(listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }
        [HttpPost]
        public async Task<IActionResult>Update(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _listingDbContext.Listings.Update(listing);
                await _listingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            return View(listing);
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var listing = await _listingDbContext.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        [HttpPost]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var listing = await _listingDbContext.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }
            _listingDbContext.Listings.Remove(listing);
            await _listingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
    }
}