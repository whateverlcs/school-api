namespace School.Domain.Repositories.Student
{
    public interface IStudentWriteOnlyRepository
    {
        Task Add(Entities.Student student);

        Task Delete(long studentId);
    }
}