namespace School.Domain.Repositories.Academy
{
    public interface IAcademyWriteOnlyRepository
    {
        Task Add(Entities.Academy academy);

        Task Delete(long academyId);
    }
}