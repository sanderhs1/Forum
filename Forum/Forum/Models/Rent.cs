namespace Forum.Models;

public class Rent
{
    public int RentId { get; set; }
    public string RentDate { get; set; } = string.Empty;
    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = default!;

    public virtual List<RentListing>? RentListings { get; set; }
    public decimal TotalPrice { get; set; }

    public virtual Listing ListingId { get; set; } = default!;
}
