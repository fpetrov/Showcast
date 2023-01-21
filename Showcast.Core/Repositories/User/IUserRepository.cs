namespace Showcast.Core.Repositories.User;

public interface IUserRepository //: IRepository<Entities.Authentication.User>
{
    public Task<bool> SignIn(Entities.Authentication.User user);
    public Task<bool> SignUp(Entities.Authentication.User user);
}