using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;


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

        public async Task<IActionResult> RentView(int ListingId)
        {
            var listings = await _listingDbContext.Listings.ToListAsync();
            var rents = await _listingDbContext.Rents.ToListAsync();
            var rentViewModel = new RentViewModel
            {
                RentListing = new RentListing { ListingId = ListingId},

                ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.ListingId.ToString() + ": " + listing.Name
                }).ToList(),

                RentSelectList = rents.Select(rent => new SelectListItem
                {
                    Value = rent.RentId.ToString(),
                    Text = "Rent" + rent.RentId.ToString() + ", Date: " + rent.CheckInDate + ", Customer: " + rent.Customer?.CustomerName ?? "Customer Not Found"
                }).ToList(),
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1)
            };
            return View(rentViewModel);
        }

        public async Task<IActionResult> RentView(RentListing rentListing)
        {
            try
            {
                DateTime checkInDate = rentListing.CheckInDate;
                DateTime checkOutDate = rentListing.CheckOutDate;
                var newListing = await _listingDbContext.Listings.FindAsync(rentListing.ListingId);
                var newRent = await _listingDbContext.Rents.FindAsync(rentListing.RentId);

                if(newListing == null || newRent == null)
                {
                    return BadRequest("Listing or Rent not found");

                }

                var newRentListing = new RentListing
                {
                    ListingId = rentListing.ListingId,
                    Listing = newListing,
                    RentId = rentListing.RentId,
                    Rent = newRent,

                };

                newRentListing.RentListingPrice = newRentListing.Listing.Price;

                if (newRentListing.Listing == null || newRentListing.Rent == null)
                {
                    var listings = await _listingDbContext.Listings.ToListAsync();
                    var rents = await _listingDbContext.Rents.ToListAsync();
                    var rentViewModel = new RentViewModel
                    {
                        RentListing = rentListing,
                        ListingSelectList = listings.Select(listing => new SelectListItem
                        {
                            Value = listing.ListingId.ToString(),
                            Text = listing.ListingId.ToString() + ": " + listing.Name
                        }).ToList(),

                        RentSelectList = rents.Select(rent => new SelectListItem
                        {
                            Value = rent.RentId.ToString(),
                            Text = "Rent" + rent.RentId.ToString() + ", Date: " + rent.CheckInDate + ", Customer: " + rent.Customer?.CustomerName ?? "Cusotmer not Found"
                        }).ToList(),
                    };
                    return View(rentViewModel);
                }
                _listingDbContext.RentListings.Add(newRentListing);
                await _listingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Table));
            }
            catch
            {
                return BadRequest("RentListing creation failed.");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RentOrder()
        {
            List<Rent> rents = await _listingDbContext.Rents.ToListAsync();
            return View(rents);
        }
    }
}