using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Repositories.Academy;

namespace School.Infrastructure.DataAccess.Repositories
{
    public class AcademyRepository : IAcademyWriteOnlyRepository, IAcademyReadOnlyRepository, IAcademyUpdateOnlyRepository
    {
        private readonly SchoolDbContext _dbContext;

        public AcademyRepository(SchoolDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Academy academy) => await _dbContext.Academys.AddAsync(academy);

        public async Task Delete(long academyId)
        {
            var academy = await _dbContext.Academys.FindAsync(academyId);

            _dbContext.Academys.Remove(academy!);
        }

        async Task<Academy?> IAcademyReadOnlyRepository.GetById(long academyId)
        {
            return await _dbContext
                .Academys
                .AsNoTracking()
                .FirstOrDefaultAsync(academy => academy.Active && academy.Id == academyId);
        }

        async Task<IList<Academy>> IAcademyReadOnlyRepository.GetByName(string academyName)
        {
            return await _dbContext
                .Academys
                .AsNoTracking()
                .Where(academy => academy.Active && academy.Name.Contains(academyName))
                .ToListAsync();
        }

        async Task<IList<Academy>> IAcademyReadOnlyRepository.GetByState(string stateName)
        {
            return await _dbContext
                .Academys
                .AsNoTracking()
                .Where(academy => academy.Active && academy.State.Contains(stateName))
                .ToListAsync();
        }

        async Task<IList<Academy>> IAcademyReadOnlyRepository.GetByCity(string cityName)
        {
            return await _dbContext
                .Academys
                .AsNoTracking()
                .Where(academy => academy.Active && academy.City.Contains(cityName))
                .ToListAsync();
        }

        async Task<IList<Academy>> IAcademyReadOnlyRepository.GetAllAcademys()
        {
            return await _dbContext
                .Academys
                .AsNoTracking()
                .ToListAsync();
        }

        async Task<Academy?> IAcademyUpdateOnlyRepository.GetById(long academyId)
        {
            return await _dbContext
                .Academys
                .FirstOrDefaultAsync(academy => academy.Active && academy.Id == academyId);
        }

        public void Update(Academy academy) => _dbContext.Academys.Update(academy);
    }
}