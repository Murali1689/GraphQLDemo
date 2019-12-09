using GraphQL.Types;
using GraphQLDemo.Data.Repositories.Interface;
using GraphQLDemo.Types;

namespace GraphQLDemo.GraphQL
{
    public class CourseManagementQuery : ObjectGraphType
    {
        public CourseManagementQuery(ICourseRepository courseRepository)
        {
            Field<ListGraphType<CourseType>>(
                "Courses",
                resolve: context => courseRepository.GetAll()
                ); ;
        }
    }
}