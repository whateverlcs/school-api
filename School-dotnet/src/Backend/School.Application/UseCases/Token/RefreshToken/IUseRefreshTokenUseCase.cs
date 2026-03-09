using School.Communication.Requests;
using School.Communication.Responses;

namespace School.Application.UseCases.Token.RefreshToken;

public interface IUseRefreshTokenUseCase
{
    Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
}