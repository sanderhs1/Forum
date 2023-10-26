using System.ComponentModel.DataAnnotations;

namespace Forum.Models;

public class Rent
{
    public int RentId { get; set; }


    [Display(Name = "Check In Date")]
    public DateTime CheckInDate { get; set; }

    [Display(Name = "Check Out Date")]
    public DateTime CheckOutDate { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { set; get; } = default!;

    public virtual List<RentListing>? RentListings { get; set; }
    public decimal TotalPrice { get; set; }

    
}
