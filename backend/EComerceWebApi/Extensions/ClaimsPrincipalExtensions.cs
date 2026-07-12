using System.Security.Claims;

namespace EComerceWebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user) //token içerisindeki id yi döndürüyor.
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
