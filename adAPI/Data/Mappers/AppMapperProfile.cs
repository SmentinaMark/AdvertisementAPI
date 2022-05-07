using adAPI.Contracts;
using adAPI.Models;
using AutoMapper;

namespace adAPI.Data.Mappers
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Advertisement, GetAdvertisements>();
            CreateMap<Advertisement, GetSingleAdvertisement>().ForMember(x => x._Images, opt => opt.Ignore()); ;

            CreateMap<Advertisement, CreateAdvertisement>();
        }
    }
}
