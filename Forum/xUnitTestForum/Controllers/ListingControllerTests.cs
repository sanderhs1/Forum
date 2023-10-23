using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Forum.Controllers;
using Forum.DAL;
using Forum.Models;
using Forum.ViewModels;


namespace xUnitTestForum.Controllers
{
    public class ListingControllerTests
    {
        [Fact]
        public async Task TestTable()
        {
            // arrange
            var itemList = new List<Listing>();
            new Listing
            {
                ListingId = 1,
                Name ="test",
                Price = 500,
                Description = "House for renting",

              
            }
        }
    }
}
