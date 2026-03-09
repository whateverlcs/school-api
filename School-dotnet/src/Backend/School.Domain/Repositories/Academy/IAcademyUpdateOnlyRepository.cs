namespace School.Domain.Repositories.Academy
{
    public interface IAcademyUpdateOnlyRepository
    {
        public Task<Entities.Academy?> GetById(long id);

        public void Update(Entities.Academy academy);
    }
}