using School.Communication.Responses;

namespace School.Application.UseCases.Academy.GetById
{
    public interface IGetAcademyByIdUseCase
    {
        Task<ResponseAcademyJson> Execute(long academyId);
    }
}