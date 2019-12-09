using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLDemo.Data.Context;
using GraphQLDemo.Data.Models;
using GraphQLDemo.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseManagementContext _databaseContext;

        public CourseRepository(CourseManagementContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _databaseContext.Course.ToListAsync(); ;
        }
    }
}