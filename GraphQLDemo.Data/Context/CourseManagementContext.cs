using GraphQLDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Data.Context
{
    public class CourseManagementContext : DbContext
    {
        public CourseManagementContext(DbContextOptions<CourseManagementContext> options) : base(options)
        {
        }

        public virtual DbSet<Course> Course { get; set; }
    }
}