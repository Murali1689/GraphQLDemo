using GraphQLDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Data.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
    }
}