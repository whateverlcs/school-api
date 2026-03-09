using Moq;
using School.Domain.Entities;
using School.Domain.Repositories.Student;

namespace CommonTestUtilities.Repositories
{
    public class StudentReadOnlyRepositoryBuilder
    {
        private readonly Mock<IStudentReadOnlyRepository> _repository;

        public StudentReadOnlyRepositoryBuilder() => _repository = new Mock<IStudentReadOnlyRepository>();

        public StudentReadOnlyRepositoryBuilder GetById(Student student)
        {
            _repository.Setup(repository => repository.GetById(student.Id)).ReturnsAsync(student);

            return this;
        }

        public StudentReadOnlyRepositoryBuilder GetByAcademyId(Student student, IList<Student> students)
        {
            _repository.Setup(repository => repository.GetByAcademyId(student.AcademyId)).ReturnsAsync(students);

            return this;
        }

        public StudentReadOnlyRepositoryBuilder GetAllStudents(IList<Student> students)
        {
            _repository.Setup(repository => repository.GetAllStudents()).ReturnsAsync(students);

            return this;
        }

        public IStudentReadOnlyRepository Build() => _repository.Object;
    }
}