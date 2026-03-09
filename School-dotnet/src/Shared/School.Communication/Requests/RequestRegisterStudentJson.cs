using School.Communication.Enums;

namespace School.Communication.Requests
{
    public class RequestRegisterStudentJson
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public Schooling Schooling { get; set; }
        public string AcademyId { get; set; } = string.Empty;
    }
}