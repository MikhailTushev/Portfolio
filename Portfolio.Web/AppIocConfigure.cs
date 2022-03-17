using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.ApplicationServices.Example;
using Portfolio.Common;
using Portfolio.Common.ImplementationStubs;
using Portfolio.Domain.Events;
using Portfolio.Web.Hubs;

namespace Portfolio.Web
{
    public class AppIocConfigure
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddMediatR(typeof(SomeHandler).GetTypeInfo().Assembly);
            services.AddSingleton<IUserService, UserServiceStub>();

            services
                .AddSingleton<IQueueProvider, NotificationQueue>()
                .AddSingleton<IQueueWatcher<IQueueMessage>, NotificationQueue>()
                .AddSingleton<INotificator, Notificator>()
                .AddTransient<INotificationService, NotificationServiceStub>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

            return services;
        }
    }
}