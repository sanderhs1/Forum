using Microsoft.AspNetCore.Mvc.Rendering;
using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels;

public class RentViewModel
{
    public RentListing RentListing { get; set; } = default!;
    public List<SelectListItem> ListingSelectList { get; set; } = default!;
    public List<SelectListItem> RentSelectList { get; set; } = default!;
    [Display (Name = "Check In Date")]
    public DateTime CheckInDate { get; set; }

    [Display(Name = "Check Out Date")]
    public DateTime CheckOutDate { get; set; }
}
