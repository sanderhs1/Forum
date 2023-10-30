using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    public class Listing
    {
       
        public int ListingId { get; set; }

        [Range(1, 100, ErrorMessage = "Number of rooms should be between 1 and 100.")]
        /*public int AntallRom { get; set; }
        public int Bad { get; set; }
        public int Area { get; set; }
        public int Floor { get; set; }
        public int Spots { get; set; }
        public string? Adresse { get; set; }*/

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Item name")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0. ")]
        public decimal Price { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? ImageUrl1 { get; set; }


        public virtual List<RentListing>? RentListings { get; set; }
    }
}