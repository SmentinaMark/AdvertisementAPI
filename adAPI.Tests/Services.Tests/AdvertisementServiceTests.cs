using adAPI.Contracts;
using adAPI.Data;
using adAPI.Data.Repositories;
using adAPI.Models;
using adAPI.Services;
using adAPI.Tests.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace adAPI.Tests.Services.Tests
{
    public class DataManagerTests
    {
        [Fact]
        public void GetItems_ReturnAdvertisements()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();

            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(x => x.GetItems()).Returns(data.GetAdvertisements());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            mockQueryManipulation.Setup(x => x.PagingItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());
            mockQueryManipulation.Setup(x => x.SortItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());

            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);

            ///Act
            var result = service.GetItems(data.SetCollectionParamaters());

            ///Assert
            result.Should().HaveCount(data.GetAdvertisements().Count);
        }

        [Fact]
        public void GetItems_ShouldCallSearchItem()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();

            var mockRepository = new Mock<IRepository<Advertisement>>();
            var mockQueryManipulation = new Mock<IQueryManipulation>();
            
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);

            ///Act
            service.GetItems(data.SetCollectionParamaters(search:"First"));

            ///Assert
            mockQueryManipulation.Verify(_ => _.SearchItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>()), Times.Once);
        }

        [Fact]
        public void GetItem_ReturnItem()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();

            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(x => x.GetItemById(It.IsAny<Guid>())).Returns(data.GetSingleAdvertisement());

            var mockQueryManipulation = new Mock<IQueryManipulation>();
            
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);

            ///Act
            var result = service.GetItemById((It.IsAny<Guid>()), It.IsAny<bool>());

            ///Assert
            Assert.True(result.Equals(fakeData[0]));
        }


        [Fact]
        public void GetItem_ShouldCallGetAdditionalFields()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();

            var mockRepository = new Mock<IRepository<Advertisement>>();
            mockRepository.Setup(x => x.GetItemById(It.IsAny<Guid>())).Returns(data.GetSingleAdvertisement());

            var mockQueryManipulation = new Mock<IQueryManipulation>();

            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);

            ///Act
            service.GetItemById(It.IsAny<Guid>(), true);

            ///Assert
            mockQueryManipulation.Verify(_ => _.GetAdditionalFields(It.IsAny<bool>(), It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public void AddItem_NewCountItems()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();
            var parameters = data.SetCollectionParamaters();

            var mockRepository = new Mock<IRepository<Advertisement>>();
            var mockQueryManipulation = new Mock<IQueryManipulation>();
           
            var service = new AdvertisementService(mockRepository.Object, mockQueryManipulation.Object);

            ///Act
            var result = service.AddItem(data.GetSingleAdvertisement());

            ///Assert
            mockRepository.Verify(_ => _.AddItem(It.IsAny<Advertisement>()), Times.Once);
            mockRepository.Verify(_ => _.Save(), Times.Once);
        }
    }
}
