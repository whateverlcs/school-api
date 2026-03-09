using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using School.Domain.Enums;
using School.Infrastructure.DataAccess;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private School.Domain.Entities.Academy _academy = default!;
        private School.Domain.Entities.Student _student = default!;
        private School.Domain.Entities.User _user = default!;
        private School.Domain.Entities.RefreshToken _refreshToken = default!;
        private string _password = string.Empty;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SchoolDbContext>));
                    if (descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<SchoolDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

                    dbContext.Database.EnsureDeleted();

                    StartDatabase(dbContext);
                });
        }

        public string GetEmail() => _user.Email;

        public string GetPassword() => _password;

        public string GetName() => _user.Name;

        public string GetRefreshToken() => _refreshToken.Value;

        public Guid GetUserIdentifier() => _user.UserIdentifier;

        public string GetAcademyId() => IdEncripterBuilder.Build().Encode(_academy.Id);

        public string GetAcademyName() => _academy.Name;

        public string GetAcademyAddress() => _academy.Address;

        public string GetAcademyState() => _academy.State;

        public string GetAcademyCity() => _academy.City;

        public string GetStudentId() => IdEncripterBuilder.Build().Encode(_student.Id);

        public string GetStudentName() => _student.Name;

        public string GetStudentSurname() => _student.Surname;

        public string GetStudentEmail() => _student.Email;

        public int GetStudentAge() => _student.Age;

        public Schooling GetStudentSchooling() => _student.Schooling;

        public string GetStudentAcademyId() => IdEncripterBuilder.Build().Encode(_student.AcademyId);

        private void StartDatabase(SchoolDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();

            _academy = AcademyBuilder.New().Build();

            _student = StudentBuilder.Build(_academy);

            _refreshToken = RefreshTokenBuilder.Build(_user);

            dbContext.Users.Add(_user);

            dbContext.Academys.Add(_academy);

            dbContext.Students.Add(_student);

            dbContext.RefreshTokens.Add(_refreshToken);

            dbContext.SaveChanges();
        }
    }
}