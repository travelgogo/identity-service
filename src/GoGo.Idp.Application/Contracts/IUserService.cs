
using GoGo.Idp.Application.Models;

namespace GoGo.Idp.Application.Contracts
{
    public interface IUserService
    {
        UserInfo?  GetUserAccount(string userName, string password);
    }
}