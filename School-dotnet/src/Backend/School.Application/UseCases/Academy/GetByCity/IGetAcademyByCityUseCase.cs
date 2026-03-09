using School.Communication.Responses;

namespace School.Application.UseCases.Academy.GetByCity
{
    public interface IGetAcademyByCityUseCase
    {
        Task<IList<ResponseAcademyJson>> Execute(string cityName);
    }
}