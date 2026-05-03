using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationDependancyInjection
    {
        public static IServiceCollection Application(this IServiceCollection Service)
        {
            Service.AddMediatR(cfg =>
              cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return Service;
        }
    }
}