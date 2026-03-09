using School.Domain.Enums;

namespace School.Domain.Entities
{
    public class Student : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public Schooling Schooling { get; set; }
        public long AcademyId { get; set; }
    }
}