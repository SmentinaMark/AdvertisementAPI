using adAPI.Contracts;
using adAPI.Controllers;
using adAPI.Models;
using adAPI.Tests.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace adAPI.Tests
{
    public class AdvertisementControllerTests
    {
        [Fact]
        public void GetAdvertisements_ShouldReturn200()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();

            dataManager.Setup(_ => _.GetItems(It.IsAny<CollectionQueryParameters>()))
                .Returns(data.GetAdvertisements());

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.GetAdvertisements(It.IsAny<CollectionQueryParameters>());

            ///Assert
            result.GetType().Should().Be(typeof(OkObjectResult));

        }

        [Fact]
        public void GetAdvertisements_ShouldReturn204()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();

            dataManager.Setup(_ => _.GetItems(It.IsAny<CollectionQueryParameters>()))
                .Returns(data.GetEmptyAdvertisements());

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.GetAdvertisements(It.IsAny<CollectionQueryParameters>());

            ///Assert
            result.GetType().Should().Be(typeof(NoContentResult));

        }

        [Fact]
        public void GetAdvertisement_ShouldReturn200()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();

            dataManager.Setup(_ => _.GetItemById(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(data.GetSingleAdvertisement());

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.GetAdvertisement(Guid.Parse("01C19C72-31C3-4A85-85BD-CA8F99A10E18"), It.IsAny<bool>());

            ///Assert
            result.GetType().Should().Be(typeof(OkObjectResult));

        }

        [Fact]
        public void GetAdvertisement_ShouldReturn404()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();

            dataManager.Setup(_ => _.GetItemById(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(data.GetSingleAdvertisement());

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.GetAdvertisement(Guid.Empty, It.IsAny<bool>());

            ///Assert
            result.GetType().Should().Be(typeof(NotFoundResult));

        }

        [Fact]
        public void PostAdvertisement_ShouldReturn201()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();
            Advertisement newAdvertisement = data.AddCorrectAdvertisement();

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.PostAdvertisement(newAdvertisement);

            ///Assert
            result.GetType().Should().Be(typeof(CreatedAtActionResult));
        }

        [Fact]
        public void PostAdvertisement_ShouldReturn400()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            Mock<IDataManager<Advertisement, CollectionQueryParameters>> dataManager = new Mock<IDataManager<Advertisement, CollectionQueryParameters>>();
            Advertisement newAdvertisement = data.AddEmptyAdvertisement();

            var controller = new AdvertisementController(dataManager.Object);

            ///Act
            var result = controller.PostAdvertisement(newAdvertisement);

            ///Assert
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }
    }
}