using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Repositories.Student;

namespace School.Infrastructure.DataAccess.Repositories
{
    public class StudentRepository : IStudentWriteOnlyRepository, IStudentReadOnlyRepository, IStudentUpdateOnlyRepository
    {
        private readonly SchoolDbContext _dbContext;

        public StudentRepository(SchoolDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Student student) => await _dbContext.Students.AddAsync(student);

        public async Task Delete(long studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);

            _dbContext.Students.Remove(student!);
        }

        async Task<Student?> IStudentReadOnlyRepository.GetById(long studentId)
        {
            return await _dbContext
                .Students
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.Active && student.Id == studentId);
        }

        async Task<IList<Student>> IStudentReadOnlyRepository.GetByAcademyId(long academyId)
        {
            return await _dbContext
                .Students
                .AsNoTracking()
                .Where(student => student.Active && student.AcademyId == academyId)
                .ToListAsync();
        }

        async Task<IList<Student>> IStudentReadOnlyRepository.GetAllStudents()
        {
            return await _dbContext
                .Students
                .AsNoTracking()
                .ToListAsync();
        }

        async Task<Student?> IStudentUpdateOnlyRepository.GetById(long studentId)
        {
            return await _dbContext
                .Students
                .FirstOrDefaultAsync(student => student.Active && student.Id == studentId);
        }

        public void Update(Student student) => _dbContext.Students.Update(student);
    }
}