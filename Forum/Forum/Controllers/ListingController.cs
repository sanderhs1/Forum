﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Forum.Models;
using Forum.ViewModels;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

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


    public async Task<IActionResult> Table(decimal? maxPrice = null, int? minRooms = null)
    {
        IQueryable<Listing> query = _listingRepository.GetAllAsQueryable();

        // Filter by maximum price if provided
        if (maxPrice.HasValue)
        {
            query = query.Where(l => l.Price <= maxPrice.Value);
        }

        // Filter by minimum number of rooms (AntallRom) if provided
        if (minRooms.HasValue)
        {
            query = query.Where(l => l.AntallRom >= minRooms.Value);
        }

        var listings = await query.ToListAsync();

        if (listings == null || !listings.Any())
        {
            _logger.LogError("[ListingController] No listings found with the specified filters.");
            return NotFound("No listings found with the specified filters.");
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
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            if (listing.Price <= 0)
            {
                ModelState.AddModelError("PricePerNight", "Price per night must be more than 0.");
                return View(listing);
            }

            bool returnOk = await _listingRepository.Create(listing);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[LisitngController] Listing creation failed {@listing}", listing);
        return View(listing);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found when updating the ListingId{ListingId:0000}", id);
            return BadRequest("Listing not found for the ListingId");
        }
        return View(listing);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Update(Listing listing)
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
    public async Task<IActionResult> Delete(int id)
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
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool returnOk = await _listingRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[ListingController] Listing deletion failed for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing deletion failed");
        }
        return RedirectToAction(nameof(Table));
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ListingView(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId:0000}", listing);
            return BadRequest("Listing not found for the ListingId");
        }

        var listings = await _listingRepository.GetAllListings();
        var rents = await _listingRepository.GetAllRents();

        var viewModel = new ListingWithRentVM
        {
            ListingDetails = listing,
            RentDetails = new CreateRentListingViewModel
            {
                RentListing = new RentListing { ListingId = listing.ListingId },
                ListingSelectList = listings.Select(l => new SelectListItem
                {
                    Value = l.ListingId.ToString(),
                    Text = $"{l.ListingId}: {l.Name}"
                }).ToList(),
                RentSelectList = rents.Select(r => new SelectListItem
                {
                    Value = r.RentId.ToString(),
                    Text = $"Rent {r.RentId}"  // fixed here
                }).ToList()
            }
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var uploadedImage = new UploadedImage
            {
                ContentType = file.ContentType
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadedImage.imageData = memoryStream.ToArray();
            }

            bool isAdded = await _listingRepository.AddUploadedImage(uploadedImage);
            if (!isAdded)
            {
                _logger.LogError("[ListingController] Failed to upload the image.");
                return View(); 
            }

            return RedirectToAction(nameof(Table));
        }

        return View();
    }
}




