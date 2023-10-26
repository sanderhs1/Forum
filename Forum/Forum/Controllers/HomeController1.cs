using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;

namespace Forum.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ListingDbContext _listingDbContext;

        public CustomerController(ListingDbContext listingDbContext)
        {
            _listingDbContext = listingDbContext;
        }
        public async Task<IActionResult> Table()
        {
            List<Customer> customers = await _listingDbContext.Customers.ToListAsync();
            return View(customers);
        }
    }
}
