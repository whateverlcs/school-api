using Microsoft.AspNetCore.Mvc;
using School.Application.UseCases.Token.RefreshToken;
using School.Communication.Requests;
using School.Communication.Responses;

namespace School.API.Controllers;

public class TokenController : SchoolBaseController
{
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(
        [FromServices] IUseRefreshTokenUseCase useCase,
        [FromBody] RequestNewTokenJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}