using System.Security.Claims;

namespace Livros.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            var result = user.FindFirst(ClaimTypes.Name)?.Value;
            
            if(result is not null)
                return result;
            else
                return "null";
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var result = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            if(result > 0)
                return result;
            else
                return 0;
        }
    }
}