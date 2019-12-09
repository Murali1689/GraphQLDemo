using GraphQLDemo.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace GraphQLDemo.Middleware
{
    public static class MigrationAndSeeding
    {
        public static void MigrateDatabaseAndSeed(this IApplicationBuilder builder)
        {
            var provider = builder.ApplicationServices;
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CourseManagementContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                    var services = scope.ServiceProvider;
                    var logger = services.GetService<ILogger<CourseManagementSeeding>>();

                    try
                    {
                        new CourseManagementSeeding().SeedAsync(appContext, logger);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}