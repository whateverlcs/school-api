using School.Domain.Entities;

namespace School.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    public Task<User> User();
}