using Moq;
using School.Domain.Repositories.Academy;

namespace CommonTestUtilities.Repositories
{
    public class AcademyWriteOnlyRepositoryBuilder
    {
        public static IAcademyWriteOnlyRepository Build()
        {
            var mock = new Mock<IAcademyWriteOnlyRepository>();

            return mock.Object;
        }
    }
}