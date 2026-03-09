using School.Communication.Requests;
using School.Communication.Responses;

namespace School.Application.UseCases.Academy.Register
{
    public interface IRegisterAcademyUseCase
    {
        public Task<ResponseAcademyJson> Execute(RequestRegisterAcademyJson request);
    }
}