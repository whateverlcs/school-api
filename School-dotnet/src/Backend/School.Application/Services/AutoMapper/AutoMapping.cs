using AutoMapper;
using School.Communication.Requests;
using School.Communication.Responses;
using Sqids;

namespace School.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    private readonly SqidsEncoder<long> _idEnconder;

    public AutoMapping(SqidsEncoder<long> idEnconder)
    {
        _idEnconder = idEnconder;

        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestRegisterAcademyJson, Domain.Entities.Academy>();

        CreateMap<RequestRegisterStudentJson, Domain.Entities.Student>()
        .ForMember(dest => dest.AcademyId,
            opt => opt.MapFrom(src =>
                _idEnconder.Decode(src.AcademyId).FirstOrDefault()
            ));
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

        CreateMap<Domain.Entities.Academy, ResponseAcademyJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)));

        CreateMap<Domain.Entities.Student, ResponseStudentJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)))
            .ForMember(dest => dest.AcademyId, config => config.MapFrom(source => _idEnconder.Encode(source.AcademyId)));
    }
}