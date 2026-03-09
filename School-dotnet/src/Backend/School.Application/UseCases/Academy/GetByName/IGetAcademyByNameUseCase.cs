using School.Communication.Responses;

namespace School.Application.UseCases.Academy.GetByName
{
    public interface IGetAcademyByNameUseCase
    {
        Task<IList<ResponseAcademyJson>> Execute(string academyName);
    }
}