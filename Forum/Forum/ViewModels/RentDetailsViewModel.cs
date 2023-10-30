using Forum.Models;

namespace Forum.ViewModels
{
    public class RentDetailsViewModel
    {
        public Rent? Rent { get; set; }
        public IEnumerable<RentListing>? RentListings { get; set; }
        public Listing? Listing { get; set; }

        public string? ErrorMessage { get; set; }
    }
}