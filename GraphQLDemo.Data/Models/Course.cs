using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Duration { get; set; }
    }
}