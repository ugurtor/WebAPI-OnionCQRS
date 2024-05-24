using Microsoft.Extensions.DependencyInjection;
using Onion.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application;
public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddTransient<ExceptionMiddleware>();

        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
    }
}
