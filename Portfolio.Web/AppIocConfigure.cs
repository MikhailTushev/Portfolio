using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.ApplicationServices.Example;
using Portfolio.Common;
using Portfolio.Common.ImplementationStubs;

namespace Portfolio.Web
{
    public class AppIocConfigure
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddMediatR(typeof(SomeHandler).GetTypeInfo().Assembly);
            services.AddTransient<INotificator, Notificator>();
            services.AddSingleton<IUserService, UserService>();
            return services;
        }
    }
}