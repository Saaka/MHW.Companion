using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MHW.Companion.API.Config
{
    public class SymetricKeyGenerator
    {
        internal static SymmetricSecurityKey CreateKey(IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var signinKey = jwtAppSettingOptions["SigninKey"];

            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signinKey));
        }
    }
}
