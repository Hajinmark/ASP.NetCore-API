using Microsoft.AspNetCore.Identity;

namespace WebAPIMastery.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
