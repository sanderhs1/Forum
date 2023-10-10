namespace Forum.Models;

public class RentListing
{
    public int RentListingId { get; set; }
    public int ListId { get; set; }
    public virtual Listing Listing { get; set; } = default!;
    public int RentId { get; set; }
    public virtual Rent Rent { get; set; } = default!;
    public decimal RentListingPrice { get; set; }
}
