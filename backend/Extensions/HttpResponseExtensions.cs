using SoftPlus_ToDo.DTOs.Auth;

namespace SoftPlus_ToDo.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AppendAuthCookies(
            this HttpResponse response,
            TokenResponseDto token
        )
        {
            response.Cookies.Append("X-Access-Token", token.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(60)
            });

            response.Cookies.Append("X-Refresh-Token", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(60)
            });
        }

        public static void ClearAuthCookies(
            this HttpResponse response
        )
        {
            response.Cookies.Delete("X-Access-Token");
            response.Cookies.Delete("X-Refresh-Token");
        }
    }
}