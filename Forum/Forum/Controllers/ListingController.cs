using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Forum.Models;
using Forum.ViewModels;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;

namespace Forum.Controllers;

public class ListingController : Controller
{
    //private readonly ListingDbContext _listingDbContext;

    private readonly InterListingRepository _listingRepository;
    private readonly ILogger<ListingController> _logger;

    public ListingController(InterListingRepository listingRepository, ILogger<ListingController> logger)
    {
        _logger = logger;
        _listingRepository = listingRepository;
    }

    //public List<Rent> RentConsole()
    //{
    //    return ListingRepository.Rents.ToList();

    //}
    

    public async Task<IActionResult>Table()
    {
        
        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _lisitngRepository.GetAll()");
            return NotFound("Listings list not found");
                
        }
        var listingListViewModel = new ListingListViewModel(listings, "Table");
        return View(listingListViewModel);
    }
    // Fjerne Grid/Table View, Endre til ett view, hvor vi stacker listing bokser oppå hverandre, ikke i et table, men table kan brukes
    public async Task<IActionResult> Grid()
    {

        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listings list not found while executing _listingRepository.GetAll()");
            return NotFound("Listings list not found");
        }
        var listingListViewModel = new ListingListViewModel(listings, "Grid");
        return View(listingListViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId: 0000}", id);
            return NotFound("Listings list not found for the ListingId");
        }
            
        return View(listing);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult>Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _listingRepository.Create(listing);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[LisitngController] Listing creation failed {@listing}", listing);
        return View(listing);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult>Update(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if(listing == null)
        {
            _logger.LogError("[ListingController] Listing not found when updating the ListingId{ListingId:0000}", id);
            return BadRequest("Listing not found for the ListingId");
        }
        return View(listing);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult>Update(Listing listing)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _listingRepository.Update(listing);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ListingController] Listing update failed {@listing}", listing);
        return View(listing);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult>Delete(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId:0000}", listing);
            return BadRequest("Listing not found for the ListingId");
        }
        return View(listing);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult>DeleteConfirmed(int id)
    {
        bool returnOk = await _listingRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[ListingController] Listing deletion failed for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing deletion failed");
        }
        return RedirectToAction(nameof(Table));
    }

    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configuring password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Configuring lockout settings
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);

    // Each user requires a unique email
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Setting the idle timeout for the session
    options.Cookie.Name = ".AdventureWorks.Session"; // Setting the name of the session cookie
    options.Cookie.IsEssential = true; // Indicating whether the session cookie is essential
});

}

