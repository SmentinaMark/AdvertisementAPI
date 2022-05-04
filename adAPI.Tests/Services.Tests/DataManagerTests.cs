using adAPI.Contracts;
using adAPI.Data;
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
    public class DataManagerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public DataManagerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public void GetItems_ReturnAdvertisements()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();
            var mock = new Mock<IQueryManipulation<Advertisement, CollectionQueryParameters>>();
            mock.Setup(x => x.PagingItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());
            mock.Setup(x => x.SortItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());
            
            _context.Advertisements.AddRange(fakeData);
            _context.SaveChanges();

            var service = new DataManager(_context, mock.Object);

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
            var mock = new Mock<IQueryManipulation<Advertisement, CollectionQueryParameters>>();
            mock.Setup(_ => _.SearchItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());
            mock.Setup(_ => _.SortItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>())).Returns(fakeData.AsQueryable());

            _context.Advertisements.AddRange(fakeData);
            _context.SaveChanges();

            var service = new DataManager(_context, mock.Object);

            ///Act
            service.GetItems(data.SetCollectionParamatersWithSearch());

            ///Assert
            mock.Verify(_ => _.SearchItems(It.IsAny<CollectionQueryParameters>(), It.IsAny<IQueryable<Advertisement>>()), Times.Once);
        }

        [Fact]
        public void GetItem_ReturnItem()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();
            var mock = new Mock<IQueryManipulation<Advertisement, CollectionQueryParameters>>();

            _context.Advertisements.AddRange(fakeData);
            _context.SaveChanges();

            var service = new DataManager(_context, mock.Object);

            ///Act
            var result = service.GetItemById(Guid.Parse("01C19C72-31C3-4A85-85BD-CA8F99A10E11"), false);

            ///Assert
            Assert.True(result.Equals(fakeData[0]));
            Assert.False(result.Equals(fakeData[1]));
        }

        [Fact]
        public void GetItem_ShouldCallGetAdditionalFields()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();
            var mock = new Mock<IQueryManipulation<Advertisement, CollectionQueryParameters>>();

            _context.Advertisements.AddRange(fakeData);
            _context.SaveChanges();

            var service = new DataManager(_context, mock.Object);

            ///Act
            service.GetItemById(Guid.Parse("01C19C72-31C3-4A85-85BD-CA8F99A10E11"), true);

            ///Assert
            mock.Verify(_ => _.GetAdditionalFields(It.IsAny<bool>(), It.IsAny<Advertisement>()), Times.Once);
        }

        [Fact]
        public void AddItem_NewCountItems()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            var fakeData = data.GetAdvertisements();
            var mock = new Mock<IQueryManipulation<Advertisement, CollectionQueryParameters>>();

            _context.Advertisements.AddRange(fakeData);
            _context.SaveChanges();

            var service = new DataManager(_context, mock.Object);

            ///Act
            service.AddItem(data.AddCorrectAdvertisement());

            ///Assert
            Assert.Equal(3, _context.Advertisements.Count());
            Assert.NotEqual(2, _context.Advertisements.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
