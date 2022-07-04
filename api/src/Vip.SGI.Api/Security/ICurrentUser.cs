using System.Security.Claims;

namespace Vip.SGI.Api.Security;

public interface ICurrentUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    bool IsAutenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetUserClaims();
    HttpContext GetHttpContext();
}