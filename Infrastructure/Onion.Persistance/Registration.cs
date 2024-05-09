using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Application.Interfaces.Repositories;
using Onion.Application.Interfaces.UnitOfWorks;
using Onion.Persistance.Context;
using Onion.Persistance.Repositories;
using Onion.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Persistance;
public static class Registration
{
    public static void AddPersistance(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        service.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        service.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        service.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
