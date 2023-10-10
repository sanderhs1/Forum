namespace Forum.Models;


public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public virtual List<Rent>? Rents { get; set; }
}
