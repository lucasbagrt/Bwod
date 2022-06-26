using AutoMapper;
using Bwod.CouponAPI.Data.ValueObjects;
using Bwod.CouponAPI.Model;

namespace Bwod.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();                
            });
            return mappingConfig;
        }
    }
}
