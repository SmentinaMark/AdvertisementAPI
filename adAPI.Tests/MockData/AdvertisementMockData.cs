using adAPI.Models;
using System;
using System.Collections.Generic;

namespace adAPI.Tests.MockData
{
    public class AdvertisementMockData
    {
        List<Advertisement> advertisements = new List<Advertisement>
        {
                new Advertisement
                {
                    Id = Guid.Parse("01C19C72-31C3-4A85-85BD-CA8F99A10E11"),
                    Title = "01C19C72-31C3-4A85-85BD-CA8F99A10E11",
                    Description = "FirstAdDescription",
                    Price = 125,
                    CreationDate = DateTime.Now
                },
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
                    CreationDate = DateTime.Now
                },
         };

        public List<Advertisement> GetAdvertisements()
        {
            return advertisements;
        }

        public List<Advertisement> GetEmptyAdvertisements()
        {
            return new List<Advertisement>();
        }

        public Advertisement GetSingleAdvertisement()
        {
            return new Advertisement
            {
                Id = Guid.Parse("01C19C72-31C3-4A85-85BD-CA8F99A10E18"),
                Title = "FirstAdTitle",
                Description = "FirstAdDescription",
                Price = 125,
                CreationDate = DateTime.Now
            };
        }

        public Advertisement AddCorrectAdvertisement()
        {
            Advertisement advertisement = new Advertisement
            {
                Id = Guid.NewGuid(),
                Title = "ThirdAdTitle",
                Description = "ThirdAdDescription",
                Price = 125,
                CreationDate = DateTime.Now

            };
           
            return advertisement;
        }

        public Advertisement AddEmptyAdvertisement()
        {
            return new Advertisement();
        }

        public CollectionQueryParameters SetCollectionParamaters()
        {
            CollectionQueryParameters parameters = new CollectionQueryParameters
            {
                PageNumber = 1,
                PageSize = 10,
                Search = "",
                Sort = ""
            };
            return parameters;
        }

        public CollectionQueryParameters SetCollectionParamatersWithSearch()
        {
            CollectionQueryParameters parameters = new CollectionQueryParameters
            {
                PageNumber = 1,
                PageSize = 10,
                Search = "First",
                Sort = ""
            };
            return parameters;
        }
    }
}
