﻿using Microsoft.EntityFrameworkCore;
using Forum.Models;


namespace Forum.DAL;

public class ListingRepository : InterListingRepository
{
    private readonly ListingDbContext _db;

    private readonly ILogger<ListingRepository> _logger;
    public ListingRepository(ListingDbContext db, ILogger<ListingRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<List<RentListing>> GetAllRents()
    {
        return await _db.RentListings.ToListAsync();  // assuming your DbSet is named RentListings
    }
    public async Task<List<Listing>> GetAllListings()
    {
        return await _db.Listings.ToListAsync();
    }
    // Warning her
    public async Task<IEnumerable<Listing>?> GetAll()
    {
        try
        {
            return await _db.Listings.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("[ListingRepository] listings ToListAsync() failed when GetAll(), error message: {e}", e.Message);
            return null;
        }
    }

    public async Task<Listing?> GetListingById(int id)
    {
        try
        {
            return await _db.Listings.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError("[ListingRepository] listing FindAsync(id) failed when GetListingById for ListingId {ListingId:0000}, error message:{e}", id, e.Message);
            return null;
        }
    }

    public async Task<bool> Create(Listing listing)
    {
        try
        {
            _db.Listings.Add(listing);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[ListingController] listing creation failed for listing {@listing}, error message: {e}", listing, e.Message);
            return false;
        }
    }

    public async Task<bool> Update(Listing listing)
    {
        try
        {
            _db.Listings.Update(listing);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception e)
        {
            _logger.LogError("[ListingController] listing FindAsync(id) failed when updating the ListingId {ListingId:0000}, error message: {e}", listing, e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var listing = await _db.Listings.FindAsync(id);
            if (listing == null)
            {
                _logger.LogError("[ListingRepository] listing not found for the ListingId {ListingId:0000}", id);
                return false;
            }

            _db.Listings.Remove(listing);
            await _db.SaveChangesAsync();
            return true;

        }

        catch(Exception e)
        { // warning her
            _logger.LogError("[ListingController] listing deletion failed for the ListingId {ListingId:0000, error message: {e}", id, e.Message);
            return false;
        }
    }
}
