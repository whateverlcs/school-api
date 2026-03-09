namespace School.Domain.Repositories.Student
{
    public interface IStudentUpdateOnlyRepository
    {
        public Task<Entities.Student?> GetById(long id);

        public void Update(Entities.Student student);
    }
}