using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Academy.GetByState
{
    public class GetAcademyByStateInvalidTokenTest : SchoolClassFixture
    {
        private const string METHOD = "academy/state";

        public GetAcademyByStateInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = RequestGetAcademyByStateBuilder
                .New()
                .WithState(string.Empty)
                .Build();

            var response = await DoPost(method: METHOD, request: request, token: "tokenInvalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestGetAcademyByStateBuilder
                .New()
                .WithState(string.Empty)
                .Build();

            var response = await DoPost(method: METHOD, request: request, token: string.Empty);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = RequestGetAcademyByStateBuilder
                .New()
                .WithState(string.Empty)
                .Build();

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

            var response = await DoPost(method: METHOD, request: request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}