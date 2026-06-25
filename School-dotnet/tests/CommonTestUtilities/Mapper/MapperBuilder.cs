using CommonTestUtilities.IdEncryption;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Services.Mapping;

namespace CommonTestUtilities.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            var idEncripter = IdEncripterBuilder.Build();

            var config = new TypeAdapterConfig();
            new AutoMapping(idEncripter).Register(config);

            return new ServiceMapper(
                new ServiceCollection().AddSingleton(config).BuildServiceProvider(),
                config
            );
        }
    }
}
