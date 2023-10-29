using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forum.Models;


public class RentListing
{
    [BindNever]
    
    public int RentListingId { get; set; }
    public int ListingId { get; set; }
    public virtual Listing Listing { get; set; } = default!;
    public int RentId { get; set; }
    public virtual Rent Rent { get; set; } = default!;
    public decimal RentListingPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
