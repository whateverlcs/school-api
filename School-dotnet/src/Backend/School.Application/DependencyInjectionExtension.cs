using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Services.AutoMapper;
using School.Application.UseCases.Academy.Delete;
using School.Application.UseCases.Academy.GetAllAcademys;
using School.Application.UseCases.Academy.GetByCity;
using School.Application.UseCases.Academy.GetById;
using School.Application.UseCases.Academy.GetByName;
using School.Application.UseCases.Academy.GetByState;
using School.Application.UseCases.Academy.Register;
using School.Application.UseCases.Academy.Update;
using School.Application.UseCases.Login.DoLogin;
using School.Application.UseCases.Student.Delete;
using School.Application.UseCases.Student.GetAllStudents;
using School.Application.UseCases.Student.GetByAcademyId;
using School.Application.UseCases.Student.GetById;
using School.Application.UseCases.Student.Register;
using School.Application.UseCases.Student.Update;
using School.Application.UseCases.Token.RefreshToken;
using School.Application.UseCases.User.ChangePassword;
using School.Application.UseCases.User.Profile;
using School.Application.UseCases.User.Register;
using School.Application.UseCases.User.Update;
using Sqids;

namespace School.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddIdEncoder(services, configuration);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            var sqids = option.GetService<SqidsEncoder<long>>()!;

            autoMapperOptions.AddProfile(new AutoMapping(sqids));
        }).CreateMapper());
    }

    private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
        });

        services.AddSingleton(sqids);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
        services.AddScoped<IDeleteAcademyUseCase, DeleteAcademyUseCase>();
        services.AddScoped<IGetAcademyByCityUseCase, GetAcademyByCityUseCase>();
        services.AddScoped<IGetAcademyByIdUseCase, GetAcademyByIdUseCase>();
        services.AddScoped<IGetAcademyByNameUseCase, GetAcademyByNameUseCase>();
        services.AddScoped<IGetAcademyByStateUseCase, GetAcademyByStateUseCase>();
        services.AddScoped<IGetAllAcademysUseCase, GetAllAcademysUseCase>();
        services.AddScoped<IRegisterAcademyUseCase, RegisterAcademyUseCase>();
        services.AddScoped<IUpdateAcademyUseCase, UpdateAcademyUseCase>();
        services.AddScoped<IDeleteStudentUseCase, DeleteStudentUseCase>();
        services.AddScoped<IGetStudentByAcademyIdUseCase, GetStudentByAcademyIdUseCase>();
        services.AddScoped<IGetStudentByIdUseCase, GetStudentByIdUseCase>();
        services.AddScoped<IGetAllStudentsUseCase, GetAllStudentsUseCase>();
        services.AddScoped<IRegisterStudentUseCase, RegisterStudentUseCase>();
        services.AddScoped<IUpdateStudentUseCase, UpdateStudentUseCase>();
    }
}