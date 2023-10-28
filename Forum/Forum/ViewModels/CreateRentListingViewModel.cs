using Microsoft.AspNetCore.Mvc.Rendering;
using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels;

public class CreateRentListingViewModel
{
    public Rent? Rent { get; set; }
    public RentListing RentListing { get; set; } = default!;
    public List<SelectListItem> ListingSelectList { get; set; } = default!;


    public List<SelectListItem> RentSelectList { get; set; } = default!;

}

