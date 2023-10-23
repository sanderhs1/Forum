using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


// Create OrderItem er vår RentView

namespace Forum.Controllers
{ 
    public class RentController : Controller
    {
        private readonly ListingDbContext _listingDbContext;

        public RentController(ListingDbContext listingDbContext)
        {
            _listingDbContext = listingDbContext;
        }
        public async Task<IActionResult> Table()
        {
            List<Rent> rents = await _listingDbContext.Rents.ToListAsync();
            return View(rents);
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> RentView()
        {
            var listings = await _listingDbContext.Listings.ToListAsync();
            var rents = await _listingDbContext.Rents.ToListAsync();
            var rentViewModel = new RentViewModel
            {
                RentListing = new RentListing(),

                ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.ListingId.ToString() + ": " + listing.Name
                }).ToList(),

                RentSelectList = rents.Select(rent => new SelectListItem
                {
                    Value = rent.RentId.ToString(),
                    Text = "Rent" + rent.RentId.ToString() + ", Date: " + rent.RentDate + ", Customer: " + rent.Customer?.CustomerName ?? "Customer Not Found"
                }).ToList(),
            };
            return View(rentViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}