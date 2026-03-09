using School.Communication.Requests;
using School.Communication.Responses;

namespace School.Application.UseCases.Student.Register
{
    public interface IRegisterStudentUseCase
    {
        public Task<ResponseStudentJson> Execute(RequestRegisterStudentJson request);
    }
}