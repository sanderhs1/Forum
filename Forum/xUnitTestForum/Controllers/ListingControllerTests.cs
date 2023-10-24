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
        mockListingRepository.Setup(repo => repo.GetAll()).ReturnsAsync(listingList);
        var mockLogger = new Mock<ILogger<ListingController>>();
        var listingController = new ListingController(mockListingRepository.Object, mockLogger.Object);



        // Act

        var result = await listingController.Table();


        // Assert


        var viewResult = Assert.IsType<ViewResult>(result);
        var listingListViewModel = Assert.IsAssignableFrom<ListingListViewModel>(viewResult.ViewData.Model);
        Assert.Equal(2, listingListViewModel.Listings.Count());
        Assert.Equal(listingList, listingListViewModel.Listings);
    }
    [Fact]
    public async Task TestCreateNotOk()
    {
        // Arrange

        var testListing = new Listing
        {
            ListingId = 1,
            Price = 200,
            Description = "Amazing House",
            ImageUrl = "/images/Stua1.jpg"
        };
        var mockListingRepository = new Mock<InterListingRepository>();
        mockListingRepository.Setup(repo => repo.Create(testListing)).ReturnsAsync(false);
        var mockLogger = new Mock<ILogger<ListingController>>();
        var listingController = new ListingController(mockListingRepository.Object, mockLogger.Object);


        // Act

        var result = await listingController.Create(testListing);

        // Assert

        var viewResult = Assert.IsType<ViewResult>(result);
        var viewListing = Assert.IsAssignableFrom<Listing>(viewResult.ViewData.Model);
        Assert.Equal(testListing, viewListing);
    }
}
