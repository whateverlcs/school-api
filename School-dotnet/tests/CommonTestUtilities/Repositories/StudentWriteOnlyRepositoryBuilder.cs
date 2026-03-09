using Moq;
using School.Domain.Repositories.Student;

namespace CommonTestUtilities.Repositories
{
    public class StudentWriteOnlyRepositoryBuilder
    {
        public static IStudentWriteOnlyRepository Build()
        {
            var mock = new Mock<IStudentWriteOnlyRepository>();

            return mock.Object;
        }
    }
}