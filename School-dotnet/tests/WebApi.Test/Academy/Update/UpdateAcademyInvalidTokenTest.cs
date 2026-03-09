using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Academy.Update
{
    public class UpdateAcademyInvalidTokenTest : SchoolClassFixture
    {
        private const string METHOD = "academy";

        public UpdateAcademyInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = RequestRegisterAcademyJsonBuilder.Build();

            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoPut($"{METHOD}/{id}", request, token: "tokenInvalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRegisterAcademyJsonBuilder.Build();

            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoPut($"{METHOD}/{id}", request, token: string.Empty);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = RequestRegisterAcademyJsonBuilder.Build();

            var id = IdEncripterBuilder.Build().Encode(1);

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

            var response = await DoPut($"{METHOD}/{id}", request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}