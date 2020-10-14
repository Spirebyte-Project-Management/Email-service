using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey;
using Convey.Auth;
using Convey.CQRS.Events;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Spirebyte.Services.Email.Application;
using Spirebyte.Services.Email.Application.Events.External;
using Spirebyte.Services.Email.Application.Services.Interfaces;
using Spirebyte.Services.Email.Infrastructure.Configuration;
using Spirebyte.Services.Email.Infrastructure.Contexts;
using Spirebyte.Services.Email.Infrastructure.Decorators;
using Spirebyte.Services.Email.Infrastructure.Exceptions;
using Spirebyte.Services.Email.Infrastructure.RazorRenderer;
using Spirebyte.Services.Email.Infrastructure.RazorRenderer.Interfaces;
using Spirebyte.Services.Email.Infrastructure.Services;

namespace Spirebyte.Services.Email.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddJwt()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddMessageOutbox(o => o.AddMongo())
                .AddMongo()
                .AddRedis()
                .AddJaeger()
                .AddWebApiSwaggerDocs()
                .AddSecurity()
                .AddEmailSender();
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwaggerDocs()
                .UseJaeger()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>()
                .UseRabbitMq()
                .SubscribeEvent<PasswordForgotten>();

            return app;
        }

        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                : null;

        internal static IDictionary<string, object> GetHeadersToForward(this IMessageProperties messageProperties)
        {
            const string sagaHeader = "Saga";
            if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
            {
                return null;
            }

            return saga is null
                ? null
                : new Dictionary<string, object>
                {
                    [sagaHeader] = saga
                };
        }

        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }

        internal static IConveyBuilder AddEmailSender(this IConveyBuilder builder)
        {
            var emailOptions = builder.GetOptions<EmailOptions>("EmailOptions");
            var urlOptions = builder.GetOptions<UrlOptions>("UrlOptions");
            builder.Services.AddSingleton(emailOptions);
            builder.Services.AddSingleton<IUrlOptions>(urlOptions);
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            return builder;

        }
    }
}
