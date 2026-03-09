using Moq;
using School.Domain.Entities;
using School.Domain.Repositories.Academy;

namespace CommonTestUtilities.Repositories
{
    public class AcademyUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IAcademyUpdateOnlyRepository> _repository;

        public AcademyUpdateOnlyRepositoryBuilder() => _repository = new Mock<IAcademyUpdateOnlyRepository>();

        public AcademyUpdateOnlyRepositoryBuilder GetById(Academy academy)
        {
            if (academy is not null)
                _repository.Setup(repository => repository.GetById(academy.Id)).ReturnsAsync(academy);

            return this;
        }

        public IAcademyUpdateOnlyRepository Build() => _repository.Object;
    }
}