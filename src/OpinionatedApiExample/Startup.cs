using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using OpinionatedApiExample.Employees;
using OpinionatedApiExample.Shared;
using OpinionatedApiExample.Shared.Rest;

namespace OpinionatedApiExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OpinionatedDbContext>(options => options.UseInMemoryDatabase("OpinionatedDatabase"));
            
            services.AddSingleton(provider => OpinionatedMapperFactory.CreateMapper());
            
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => {
                //Serializes enums as strings instead of numbers
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            
            var builder = new ContainerBuilder();
            builder.Populate(services);
            
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(RestGetListHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(RestGetListRequest<,>));
            builder.RegisterGeneric(typeof(RestSingleGetHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(RestSingleGetRequest<,>));
            builder.RegisterGeneric(typeof(RestPostHandler<,,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(RestPostRequest<,,>));
            builder.RegisterGeneric(typeof(RestPutHandler<,>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(RestPutRequest<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
