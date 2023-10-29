using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models;

public class Rent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RentId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer Customer { set; get; } = default!;

    public virtual List<RentListing>? RentListings { get; set; }
    public decimal TotalPrice { get; set; }


}
