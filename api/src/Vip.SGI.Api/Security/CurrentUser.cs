using System.Security.Claims;

namespace Vip.SGI.Api.Security;

public class CurrentUser : ICurrentUser
{
    #region Propriedades

    private readonly IHttpContextAccessor _accessor;

    #endregion

    #region Construtores

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    #endregion

    #region Métodos Públicos

    public string Name => _accessor.HttpContext?.User?.Identity?.Name;

    public Guid GetUserId() => IsAutenticated() ? Guid.Parse(_accessor.HttpContext!.User.GetUserId()) : Guid.Empty;

    public string GetUserEmail() => IsAutenticated() ? _accessor.HttpContext?.User.GetUserEmail() : "";

    public bool IsAutenticated() => _accessor.HttpContext!.User.Identity!.IsAuthenticated;
    public bool IsNotAutenticated() => !IsAutenticated();

    public bool IsInRole(string role) => _accessor.HttpContext!.User.IsInRole(role);

    public IEnumerable<Claim> GetUserClaims() => _accessor.HttpContext?.User.Claims;

    public HttpContext GetHttpContext() => _accessor.HttpContext;

    #endregion
}