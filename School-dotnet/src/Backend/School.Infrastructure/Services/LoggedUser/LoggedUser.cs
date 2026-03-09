using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Security.Tokens;
using School.Domain.Services.LoggedUser;
using School.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace School.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly SchoolDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(SchoolDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}