using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using School.Exceptions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Academy.GetByName
{
    public class GetAcademyByNameTest : SchoolClassFixture
    {
        private const string METHOD = "academy/name";

        private readonly Guid _userIdentifier;
        private readonly string _name;

        public GetAcademyByNameTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _name = factory.GetAcademyName();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestGetAcademyByNameBuilder.New().WithName(_name).Build();

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(method: METHOD, request: request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
            responseData.RootElement.EnumerateArray().Should().NotBeEmpty();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Academy_Not_Found(string culture)
        {
            var request = RequestGetAcademyByNameBuilder.New().WithName("NotExists").Build();

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(method: METHOD, request: request, token: token, culture: culture);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("ACADEMY_NOT_FOUND", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}