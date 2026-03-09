namespace School.Domain.Repositories.Academy
{
    public interface IAcademyReadOnlyRepository
    {
        Task<Entities.Academy?> GetById(long academyId);

        Task<IList<Entities.Academy>> GetByName(string academyName);

        Task<IList<Entities.Academy>> GetByState(string stateName);

        Task<IList<Entities.Academy>> GetByCity(string cityName);

        Task<IList<Entities.Academy>> GetAllAcademys();
    }
}