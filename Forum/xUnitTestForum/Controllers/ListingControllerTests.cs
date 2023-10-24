using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Forum.Controllers;
using Forum.DAL;
using Forum.Models;
using Forum.ViewModels;


namespace xUnitTestForum.Controllers;

public class ListingControllerTests
{
    [Fact]
    public async Task TestTable()
    {
        // arrange
        var listingList = new List<Listing>()
        {
            new Listing
            {
                ListingId = 1,
                Name ="Nice house",
                Price = 400,
                Description = "Nice house with windows",
                ImageUrl = "/images/Hus1.jpg"
            },
            new Listing
            {
                ListingId = 2,
                Name = "Bad House",
                Price = 100,
                Description="Bad house without windows",
                ImageUrl = "/images/Kitchen1.jpg"
            }
        };

        var mockListingRepository = new Mock<InterListingRepository>();
        mockListingRepository.Setup(repo => repo.GetAll()).ReturnAsync(listingList);
        var mockLogger = new Mock<ILogger<ListingController>>();
        var listingCOntroller = new ListingControllerTests(mockListingRepository.Object, mockLogger.Object);



        // Act

        var result = await listingCOntroller.Table();


        // Assert


        var viewResult = Assert.IsType<viewResult>(result);
        var listingListViewModel = Assert.IsAssignableFrom<ListingListViewModel>(viewResult.ViewData.Model);
        Assert.Equal(2, listingListViewModel.Listings.Count());
        Assert.Equal(listingList, listingListViewModel.Listings);
    }
}
