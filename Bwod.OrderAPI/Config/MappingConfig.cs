using AutoMapper;
using Bwod.OrderAPI.Data.ValueObjects;
using Bwod.OrderAPI.Model;

namespace Bwod.OrderAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.CreateMap<OrderHeaderVO, OrderHeader>().ReverseMap();                
            });
            return mappingConfig;
        }
    }
}
