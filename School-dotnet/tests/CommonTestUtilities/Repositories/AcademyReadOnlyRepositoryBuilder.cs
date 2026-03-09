using Moq;
using School.Domain.Entities;
using School.Domain.Repositories.Academy;

namespace CommonTestUtilities.Repositories
{
    public class AcademyReadOnlyRepositoryBuilder
    {
        private readonly Mock<IAcademyReadOnlyRepository> _repository;

        public AcademyReadOnlyRepositoryBuilder() => _repository = new Mock<IAcademyReadOnlyRepository>();

        public AcademyReadOnlyRepositoryBuilder GetById(Academy academy)
        {
            _repository.Setup(repository => repository.GetById(academy.Id)).ReturnsAsync(academy);

            return this;
        }

        public AcademyReadOnlyRepositoryBuilder GetByName(Academy academy, IList<Academy> academies)
        {
            _repository.Setup(repository => repository.GetByName(academy.Name)).ReturnsAsync(academies);

            return this;
        }

        public AcademyReadOnlyRepositoryBuilder GetByState(Academy academy, IList<Academy> academies)
        {
            _repository.Setup(repository => repository.GetByState(academy.State)).ReturnsAsync(academies);

            return this;
        }

        public AcademyReadOnlyRepositoryBuilder GetByCity(Academy academy, IList<Academy> academies)
        {
            _repository.Setup(repository => repository.GetByCity(academy.City)).ReturnsAsync(academies);

            return this;
        }

        public AcademyReadOnlyRepositoryBuilder GetAllAcademys(IList<Academy> academies)
        {
            _repository.Setup(repository => repository.GetAllAcademys()).ReturnsAsync(academies);

            return this;
        }

        public IAcademyReadOnlyRepository Build() => _repository.Object;
    }
}