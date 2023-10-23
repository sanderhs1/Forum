using Microsoft.AspNetCore.Mvc.Rendering;
using Forum.Models;


namespace Forum.ViewModels;

public class RentViewModel
{
    public RentListing RentListing { get; set; } = default!;
    public List<SelectListItem> ListingSelectList { get; set; } = default!;
    public List<SelectListItem> RentSelectList { get; set; } = default!;
}
