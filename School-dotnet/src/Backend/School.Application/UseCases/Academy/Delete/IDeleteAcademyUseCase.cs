namespace School.Application.UseCases.Academy.Delete
{
    public interface IDeleteAcademyUseCase
    {
        Task Execute(long academyId);
    }
}