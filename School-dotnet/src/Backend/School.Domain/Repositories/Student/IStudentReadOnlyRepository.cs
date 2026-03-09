namespace School.Domain.Repositories.Student
{
    public interface IStudentReadOnlyRepository
    {
        Task<Entities.Student?> GetById(long studentId);

        Task<IList<Entities.Student>> GetByAcademyId(long academyId);

        Task<IList<Entities.Student>> GetAllStudents();
    }
}