using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.GraphQL
{
    public class CourseManagementSchema : Schema
    {
        public CourseManagementSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CourseManagementQuery>();
        }
    }
}