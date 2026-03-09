using Moq;
using School.Domain.Entities;
using School.Domain.Repositories.Student;

namespace CommonTestUtilities.Repositories
{
    public class StudentUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IStudentUpdateOnlyRepository> _repository;

        public StudentUpdateOnlyRepositoryBuilder() => _repository = new Mock<IStudentUpdateOnlyRepository>();

        public StudentUpdateOnlyRepositoryBuilder GetById(Student student)
        {
            if (student is not null)
                _repository.Setup(repository => repository.GetById(student.Id)).ReturnsAsync(student);

            return this;
        }

        public IStudentUpdateOnlyRepository Build() => _repository.Object;
    }
}