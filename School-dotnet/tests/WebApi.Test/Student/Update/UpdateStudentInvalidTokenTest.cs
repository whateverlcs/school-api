using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Student.Update
{
    public class UpdateStudentInvalidTokenTest : SchoolClassFixture
    {
        private const string METHOD = "student";

        public UpdateStudentInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = RequestRegisterStudentJsonBuilder.Build(string.Empty);

            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoPut($"{METHOD}/{id}", request, token: "tokenInvalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestRegisterStudentJsonBuilder.Build(string.Empty);

            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoPut($"{METHOD}/{id}", request, token: string.Empty);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = RequestRegisterStudentJsonBuilder.Build(string.Empty);

            var id = IdEncripterBuilder.Build().Encode(1);

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

            var response = await DoPut($"{METHOD}/{id}", request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}