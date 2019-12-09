using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Data.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Location { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }
}