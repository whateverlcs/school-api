using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Academy.GetAllAcademys
{
    public class GetAllAcademysTest : SchoolClassFixture
    {
        private const string METHOD = "academy";

        private readonly Guid _userIdentifier;

        public GetAllAcademysTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoGet(METHOD, token);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
            responseData.RootElement.EnumerateArray().Should().NotBeEmpty();
        }
    }
}