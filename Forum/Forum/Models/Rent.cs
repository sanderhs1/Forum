using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models;

public class Rent
{

    public int RentId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { set; get; } = default!;

    public virtual List<RentListing>? RentListings { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal RentListingPrice { get; set; }


}
