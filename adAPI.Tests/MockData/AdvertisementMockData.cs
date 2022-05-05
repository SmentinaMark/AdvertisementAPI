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
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
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
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
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
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
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
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
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
                new Advertisement
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondAdTitle",
                    Description = "SecondAdDescription",
                    Price = 14124,
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

        public Advertisement GetEmptyAdvertisement()
        {
            return new Advertisement();
        }

        public CollectionQueryParameters SetCollectionParamaters(int page = 1, int pageSize = 10, string search = "", string sort = "")
        {
            CollectionQueryParameters parameters = new CollectionQueryParameters
            {
                PageNumber = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort
            };
            return parameters;
        }
    }
}
