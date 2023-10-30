using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;


namespace Forum.Controllers
{ 
    public class RentController : Controller
    {
        // Reference to database context
        private readonly ListingDbContext _listingDbContext;

        // Logger to handle logger operations
        private readonly ILogger<RentController> _logger;
        
        // Constructor for RentController
        public RentController(ListingDbContext listingDbContext, ILogger<RentController> logger)
        {
            _listingDbContext = listingDbContext;
            _logger = logger;
        }

        // This action returns the view and displays a table of rent listing
        public async Task<IActionResult> Table()
        {

            // Retrieve all rent listings from the database
            List<Rent> rents = await _listingDbContext.Rents.ToListAsync();

            // Check if the retrieved list of rents is null
            if (rents == null || !rents.Any())
            {
                _logger.LogError("[RentController] No rents found.");
            }
            return View(rents);
        }
        [HttpGet]
        public async Task<IActionResult> RentDetails(int rentId)
        {

            // Fetch rent listings and its details
            var rent = await _listingDbContext.Rents
                .Include(r => r.RentListings)
                .ThenInclude(rl => rl.Listing)
                .FirstOrDefaultAsync(r => r.RentId == rentId);


            // Cehck if the retrieved rent is null and log the error
            if (rent == null)
            {
                _logger.LogWarning("[RentController] Rent not found for RentId {RentId}", rentId);
                return NotFound();
            }

            // Construct a view model and fetch rent and its details
            var viewModel = new RentDetailsViewModel
            {
                Rent = rent,
                RentListings = rent.RentListings,
                Listing = rent.RentListings.FirstOrDefault()?.Listing
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> CreateRentListing(int ListingId)
        {

            // Fetch the listings from the database
            var listings = await _listingDbContext.Listings.ToListAsync();

            // Fetching rents from database
            var rents = await _listingDbContext.Rents.ToListAsync();

            // Construct view model for creating a rent listing 
            var createRentListingViewModel = new CreateRentListingViewModel
            {

                // Initialize the rentlisting property iwth listing id
                RentListing = new RentListing { ListingId = ListingId},

                ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    // This will show for listing and combinations of its IDs and name
                    Value = listing.ListingId.ToString(),
                    Text = listing.ListingId.ToString() + ": " + listing.Name
                }).ToList(),

                // Convert the list of rents to list of SelectListItem
                RentSelectList = rents.Select(rent => new SelectListItem
                {
                    Value = rent.RentId.ToString(),

                    // If the Customer is not its error is shown
                    Text = "Rent" + rent.RentId.ToString()   + ", Customer: " + rent.Customer?.CustomerName ?? "Customer Not Found"
                }).ToList(),

            };
            return View(createRentListingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRentListing(RentListing rentListing)
        {
            // Validate the EndDate
            if (rentListing.EndDate <= rentListing.StartDate)
            {

                // Log a warning
                _logger.LogWarning("[RentController] End date {EndDate} is not greater than start date {StartDate}", rentListing.EndDate, rentListing.StartDate);
                var rentDetailsViewModel = new RentDetailsViewModel
                {
                    // Set error message for the user
                  
                    ErrorMessage = "End date must be greater than start date."
                };

             
                return View("RentDetails", rentDetailsViewModel);
            }
            try
            {
                // Retrieve the listing and rent and its details
                var newListing = await _listingDbContext.Listings.FindAsync(rentListing.ListingId);
                var newRent = await _listingDbContext.Rents.FindAsync(rentListing.RentId);
                _logger.LogInformation("[RentController] RentListing created successfully for ListingId {ListingId} and RentId {RentId}", rentListing.ListingId, rentListing.RentId);



                // Ensure both listing and rent exist if not it will send BadRequest
                if (newListing == null || newRent == null)
                {
                    return BadRequest("Listing or Rent not found");
                }

                // Construct a new RentListing entity
                var newRentListing = new RentListing
                {
                    ListingId = rentListing.ListingId,
                    Listing = newListing,
                    RentId = rentListing.RentId,
                    StartDate = rentListing.StartDate,
                    EndDate = rentListing.EndDate,
                    Rent = newRent
                };

                
                // Calculate the price
                int daysStayed = (newRentListing.EndDate - newRentListing.StartDate).Days;
                newRentListing.RentListingPrice = daysStayed * newRentListing.Listing.Price;

                // Ensure that the listing is available 
                if (newRentListing.Listing == null || newRentListing.Rent == null)
                {
                    var listings = await _listingDbContext.Listings.ToListAsync();
                    var rents = await _listingDbContext.Rents.ToListAsync();
                    var createRentListingViewModel = new CreateRentListingViewModel
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
                            Text = "Rent" + rent.RentId.ToString() +  ", Customer: " + rent.Customer?.CustomerName ?? "Cusotmer not Found"
                        }).ToList(),
                    };
                    return View(createRentListingViewModel);
                }

                // Save new RentListing to the database
                _listingDbContext.RentListings.Add(newRentListing);
                await _listingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(RentDetails), new { rentId = newRentListing.RentId });
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                _logger.LogError(ex, "[RentController] RentListing creation failed due to an exception.");
                // Return an error 
                return BadRequest("RentListing creation failed.");
            }
        }
    }
}