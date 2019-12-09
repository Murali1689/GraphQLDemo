using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLDemo.Data.Context;
using GraphQLDemo.Data.Repositories;
using GraphQLDemo.GraphQL;
using GraphQLDemo.Middleware;
using GraphQLDemo.Types;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace GraphQLDemo
{
    public class Startup
    {
        private const string CourseManagementDatabase = nameof(CourseManagementDatabase);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(CourseManagementDatabase);
            services.AddDbContext<CourseManagementContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                          10,
                          TimeSpan.FromSeconds(30),
                          null
                      );
                });
                options.EnableSensitiveDataLogging(true);
            });
            //TODO: use dependency injection

            services.AddScoped<CourseRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CourseType>();
            services.AddScoped<CourseManagementSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = true; });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //.AddGraphTypes(ServiceLifetime.Scoped).AddUserContextBuilder(httpContext => httpContext.User)
            //.AddDataLoader()
            //.AddWebSockets();
        }

        public void ConfigureContainer(ServiceRegistry services)
        {
            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.AssembliesFromApplicationBaseDirectory(a => a.GetName().Name.Contains("GraphQLDemo"));
                s.WithDefaultConventions();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            // app.UseGraphQLWebSockets<CarvedRockSchema>("/graphql");
            app.UseGraphQL<CourseManagementSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.MigrateDatabaseAndSeed();
            //using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    //migrate database
            //    scope.ServiceProvider.GetService<CourseManagementContext>().MigrateDatabase();

            //    //seed initial data
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<CourseManagementContext>();
            //    var logger = services.GetService<ILogger<CourseManagementSeeding>>();

            //    new CourseManagementSeeding().SeedAsync(context, logger);
            //}
        }
    }
}