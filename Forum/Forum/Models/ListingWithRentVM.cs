using Forum.Models;
using Forum.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ListingWithRentVM
{
    public Listing ListingDetails { get; set; }
    public CreateRentListingViewModel RentDetails { get; set; }
    public List<SelectListItem> ListingSelectList { get; set; }
    public List<SelectListItem> RentSelectList { get; set; }
}
