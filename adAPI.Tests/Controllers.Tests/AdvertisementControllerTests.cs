using adAPI.Contracts;
using adAPI.Controllers;
using adAPI.Data.Repositories;
using adAPI.Models;
using adAPI.Services;
using adAPI.Tests.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
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
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.GetItems()).Returns(data.GetAdvertisements());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            mockQueryManipulation.Setup(_ => _.PagingItems(It.IsAny<CollectionQueryParameters>(), 
                It.IsAny<IQueryable<Advertisement>>())).Returns(data.GetAdvertisements().AsQueryable());
            mockQueryManipulation.Setup(_ => _.SortItems(It.IsAny<CollectionQueryParameters>(),
               It.IsAny<IQueryable<Advertisement>>())).Returns(data.GetAdvertisements().AsQueryable());

            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

            ///Act
            var result = controller.GetAdvertisements(data.SetCollectionParamaters());

            ///Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public void GetAdvertisements_ShouldReturn204()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.GetItems()).Returns(data.GetEmptyAdvertisements());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            mockQueryManipulation.Setup(_ => _.PagingItems(It.IsAny<CollectionQueryParameters>(),
                It.IsAny<IQueryable<Advertisement>>())).Returns(data.GetEmptyAdvertisements().AsQueryable());
            mockQueryManipulation.Setup(_ => _.SortItems(It.IsAny<CollectionQueryParameters>(),
               It.IsAny<IQueryable<Advertisement>>())).Returns(data.GetEmptyAdvertisements().AsQueryable());

            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

            ///Act
            var result = controller.GetAdvertisements(data.SetCollectionParamaters());

            ///Assert
            result.GetType().Should().Be(typeof(NoContentResult));
        }


        [Fact]
        public void GetAdvertisement_ShouldReturn200()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.GetItemById(It.IsAny<Guid>())).Returns(data.GetSingleAdvertisement());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

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
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.GetItemById(It.IsAny<Guid>())).Returns(data.GetSingleAdvertisement());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

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
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.AddItem(It.IsAny<Advertisement>()));

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

            ///Act
            var result = controller.PostAdvertisement(data.GetSingleAdvertisement());

            ///Assert
            result.GetType().Should().Be(typeof(CreatedAtActionResult));
        }

        [Fact]
        public void PostAdvertisement_ShouldReturn400()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(_ => _.AddItem(It.IsAny<Advertisement>()));

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);
            var controller = new AdvertisementController(service);

            ///Act
            var result = controller.PostAdvertisement(data.GetEmptyAdvertisement());

            ///Assert
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }
    }
}