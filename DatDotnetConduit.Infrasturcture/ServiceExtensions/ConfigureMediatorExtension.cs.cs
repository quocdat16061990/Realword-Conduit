using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Infrasturcture.ServiceExtensions
{
   
    public static class ConfigureMediatorExtension
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("DatDotnetConduit.Application");
            services.AddMediatR(assembly);
        }
    }
}

