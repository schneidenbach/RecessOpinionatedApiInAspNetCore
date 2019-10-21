using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreWorkshop.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAuthentication();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(typeof(GlobalExceptionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<WorkshopDbContext>(options => options.UseInMemoryDatabase("test"));
            services.AddSingleton(WorkshopMapper.CreateMapper());
            services.AddAuthentication().AddJwtBearer();

            var autofac = GetAutofacContainer(GetType().Assembly);
            autofac.Populate(services);

            return new AutofacServiceProvider(autofac.Build());
        }

        public static ContainerBuilder GetAutofacContainer(Assembly assembly)
        {
            var builder = new ContainerBuilder();

            // the in-memory delegate handler for requests
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            // this is the service resolver that the Mediator class above needs to create services
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // registers all IRequest and IRequestHandlers from the executing assemblies, as well as validators
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            return builder;
        }
    }
}


