using School.Communication.Responses;

namespace School.Application.UseCases.Academy.GetByState
{
    public interface IGetAcademyByStateUseCase
    {
        Task<IList<ResponseAcademyJson>> Execute(string stateName);
    }
}