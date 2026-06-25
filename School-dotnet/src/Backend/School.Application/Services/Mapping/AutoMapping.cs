using Mapster;
using School.Communication.Requests;
using School.Communication.Responses;
using Sqids;

namespace School.Application.Services.Mapping;

public class AutoMapping : IRegister
{
    private readonly SqidsEncoder<long> _idEncoder;

    public AutoMapping(SqidsEncoder<long> idEncoder)
    {
        _idEncoder = idEncoder;
    }

    public void Register(TypeAdapterConfig config)
    {
        RequestToDomain(config);
        DomainToResponse(config);
    }

    private void RequestToDomain(TypeAdapterConfig config)
    {
        config
            .NewConfig<RequestRegisterUserJson, Domain.Entities.User>()
            .Ignore(dest => dest.Password);

        config.NewConfig<RequestRegisterAcademyJson, Domain.Entities.Academy>();

        config
            .NewConfig<RequestRegisterStudentJson, Domain.Entities.Student>()
            .Map(dest => dest.AcademyId, src => _idEncoder.Decode(src.AcademyId).FirstOrDefault());
    }

    private void DomainToResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.User, ResponseUserProfileJson>();

        config
            .NewConfig<Domain.Entities.Academy, ResponseAcademyJson>()
            .Map(dest => dest.Id, src => _idEncoder.Encode(src.Id));

        config
            .NewConfig<Domain.Entities.Student, ResponseStudentJson>()
            .Map(dest => dest.Id, src => _idEncoder.Encode(src.Id))
            .Map(dest => dest.AcademyId, src => _idEncoder.Encode(src.AcademyId));
    }
}
