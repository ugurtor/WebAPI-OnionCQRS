using Microsoft.Extensions.DependencyInjection;
using Onion.Application.Interfaces.AutoMapper;

namespace Onion.Mapper;
public static class Registration
{
    public static void AddCustomMapper(this IServiceCollection service)
    {
        service.AddSingleton<IMapper, AutoMapper.Mapper>();
    }
}
