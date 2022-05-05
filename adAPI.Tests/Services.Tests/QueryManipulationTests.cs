using adAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace adAPI.Tests.Services.Tests
{
    public class QueryManipulationTests
    {

        [Fact]
        public void SearchItems_ReturnSingleCollection()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            QueryManipulation manipulation = new QueryManipulation();

            var parameters = data.SetCollectionParamaters(search:"Fir");
            var fakeData = data.GetAdvertisements();

            ///Act
            var result = manipulation.SearchItems(parameters, fakeData.AsQueryable());

            ///Assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void SearchItems_ReturnEmptyCollection()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            QueryManipulation manipulation = new QueryManipulation();

            var parameters = data.SetCollectionParamaters(search:"kek");
            var fakeData = data.GetAdvertisements();

            ///Act
            var result = manipulation.SearchItems(parameters, fakeData.AsQueryable());

            ///Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PagingItems_ReturnPagedItems()
        {
            ///Arrange
            AdvertisementMockData data = new AdvertisementMockData();
            QueryManipulation manipulation = new QueryManipulation();

            var firstPageParams = data.SetCollectionParamaters(page:1 );
            var secondPageParams = data.SetCollectionParamaters(page:2 );

            var fakeData = data.GetAdvertisements();

            ///Act
            var resultFirstPage = manipulation.PagingItems(firstPageParams, fakeData.AsQueryable());
            var resultSecondPage = manipulation.PagingItems(secondPageParams, fakeData.AsQueryable());

            ///Assert
            Assert.Equal(10, resultFirstPage.Count());
            Assert.Equal(2, resultSecondPage.Count());
        }
    }
}
