using GraphQLDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Data.Context
{
    public class CourseManagementSeeding
    {
        public IList<Course> courses = new List<Course>() {
        new Course{
       // Id =1,
        Title ="Leadership",
        Description ="Course Description",
        Duration=5
        },
        new Course{
       // Id =2,
        Title ="Leadership and Managemetn",
        Description ="Course Description",
        Duration=7
        },
        new Course{
       // Id =3,
        Title ="Technoology",
        Description ="Course Description",
        Duration=5
        },
        new Course{
        //Id =4,
        Title ="Process",
        Description ="Course Description",
        Duration=3
        }
        };

        public void SeedAsync(CourseManagementContext context, ILogger<CourseManagementSeeding> logger)
        {
            logger.LogInformation("Executing Seeding");
            var retryPolicy = CreatePolicy(logger, nameof(CourseManagementContext));
            retryPolicy.Execute(() => { AddOrUpdateAsync(context); });
            logger.LogInformation("Completed Seeding");
        }

        private void AddOrUpdateAsync(CourseManagementContext context)
        {
            if (!context.Course.Any())
            {
                try
                {
                    context.Course.AddRange(courses);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private RetryPolicy CreatePolicy(ILogger<CourseManagementSeeding> logger, string prefix, int retries = 3)
        {
            return Policy
                .Handle<SqlException>()
                .WaitAndRetry(
                retries,
                retryAttempt => TimeSpan.FromSeconds(5),
                (exception, timespan, context) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message}", prefix, exception.GetType().Name, exception.Message);
                });
        }
    }
}