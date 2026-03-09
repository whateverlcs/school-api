using School.Domain.Security.Tokens;
using School.Infrastructure.Security.Tokens.Refresh;

namespace CommonTestUtilities.Tokens
{
    public class RefreshTokenGeneratorBuilder
    {
        public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
    }
}