using System;
using System.Collections.Generic;
using Forum.Models;
//test
namespace Forum.ViewModels
{
    public class ListingListViewModel
    {
        public IEnumerable<Listing> Listings;
        public string? CurrentViewName;
        public string? ErrorMessage { get; set; }

        public ListingListViewModel(IEnumerable<Listing> listings, string? currentViewName)
        {
            Listings = listings;
            CurrentViewName = currentViewName;
        }
    }
}