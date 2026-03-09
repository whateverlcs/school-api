using Microsoft.AspNetCore.Mvc;
using School.Application.UseCases.Login.DoLogin;
using School.Communication.Requests;
using School.Communication.Responses;

namespace School.API.Controllers;

public class LoginController : SchoolBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}