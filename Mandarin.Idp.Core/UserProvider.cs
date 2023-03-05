using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Models;

namespace Mandarin.Idp.Core;

public class UserProvider: IUserProvider
{
    public CurrentUser? CurrentUser { get; set; }

    public void SetUser(CurrentUser? user)
    {
        CurrentUser = user;
    }
}