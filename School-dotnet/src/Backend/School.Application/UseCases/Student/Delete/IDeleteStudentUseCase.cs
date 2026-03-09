namespace School.Application.UseCases.Student.Delete
{
    public interface IDeleteStudentUseCase
    {
        Task Execute(long studentId);
    }
}