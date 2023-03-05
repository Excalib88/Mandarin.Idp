using Mandarin.Idp.Contracts.Models;

namespace Mandarin.Idp.Contracts;

public interface IUserProvider
{
    public CurrentUser? CurrentUser { get; set; }
    public void SetUser(CurrentUser user);
}