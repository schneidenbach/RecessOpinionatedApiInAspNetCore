using AutoMapper;
using AutoMapper.Attributes;

namespace OpinionatedApiExample.Shared
{
    public static class OpinionatedMapperFactory
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(config => {
                typeof(OpinionatedMapperFactory).Assembly.MapTypes(config);
            }).CreateMapper();
        }
    }
}