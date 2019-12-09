using GraphQLDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;

namespace GraphQLDemo.Data.Context
{
    public class CourseManagementContext : DbContext
    {
        public CourseManagementContext(DbContextOptions<CourseManagementContext> options) : base(options)
        {
        }

        public CourseManagementContext()
        {
        }

        public virtual DbSet<Course> Course { get; set; }

        public void MigrateDatabase()
        {
            Policy.Handle<Exception>()
                    .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                    .Execute(() => Database.Migrate());
        }
    }
}