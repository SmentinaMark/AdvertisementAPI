using adAPI.Contracts;
using adAPI.Contracts.Requests;
using adAPI.Data.Models;
using AutoMapper;

namespace adAPI.Data.Mappers
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            #region Responses
            CreateMap<Advertisement, GetAdvertisements>()
               .ForMember(d => d._Images, opts => opts.MapFrom(s => s.Images.ToArray()));
            CreateMap<Advertisement, GetSingleAdvertisement>().ForMember(x => x._Images, opt => opt.Ignore());
            CreateMap<Advertisement, GetCreateAdvertisement>();
            #endregion

            #region Requests
            CreateMap<CreateAdvertisement, Advertisement>()
                .ForMember(d => d.Images, opts => opts.MapFrom(s => String.Format("[\"{0}\"]", String.Join("\",\"", s._Images))));
            #endregion
        }
    }
}
