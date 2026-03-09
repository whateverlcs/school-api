using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Student.GetAllStudents
{
    public class GetAllStudentsTest : SchoolClassFixture
    {
        private const string METHOD = "student";

        private readonly Guid _userIdentifier;

        public GetAllStudentsTest(CustomWebApplicationFactory factory) : base(factory)
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