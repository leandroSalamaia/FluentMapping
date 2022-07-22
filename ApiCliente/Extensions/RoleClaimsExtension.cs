using ApiCliente.Models.User;
using System.Linq;
using System.Security.Claims;

namespace ApiCliente.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user) {
            var result = new List<Claim> { 
                new(ClaimTypes.Name, user.Email)
            };

            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return result;
        }
    }
}
