using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using School.Domain.Extensions;

namespace School.API.Controllers;

[Route("[controller]")]
[ApiController]
public class SchoolBaseController : ControllerBase
{
    protected static bool IsNotAuthenticated(AuthenticateResult authenticate)
    {
        return authenticate.Succeeded.IsFalse()
            || authenticate.Principal is null
            || authenticate.Principal.Identities.Any(id => id.IsAuthenticated).IsFalse();
    }
}