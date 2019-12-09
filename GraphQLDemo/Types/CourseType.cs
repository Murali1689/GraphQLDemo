using GraphQL.Types;
using GraphQLDemo.Data.Models;

namespace GraphQLDemo.Types
{
    public class CourseType : ObjectGraphType<Course>

    {
        public CourseType()
        {
            Field(o => o.Id);
            Field(o => o.Title);
            Field(o => o.Description);
            Field(o => o.Duration);
        }
    }
}