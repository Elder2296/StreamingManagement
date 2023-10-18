using Microsoft.IdentityModel.Tokens;
using StreamingManagement.Controllers.Authorization;
using StreamingManagement.Models.dto.Authorization;
using StreamingManagement.Models.dto.Response;
using StreamingManagement.Utils.Consultas.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StreamingManagement.Utils.Auth
{
    public class Authenticator
    {
        private FunctionPLSQL _functionExecutor;
        private readonly IConfiguration _configuration;
        public Authenticator() {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _functionExecutor = new FunctionPLSQL();
        }
        public ResponseAuth validarInicio (User user) {
            AuthenticatorRes res = new AuthenticatorRes();

            res = _functionExecutor.validarInicioSesion(user);

            if (res.authId == 0)
            {
                return new ResponseAuth
                {
                    usuario = user.username,
                    token = "Datos no invalidos",
                    fechaExpiracion = new DateTime()

                };
            }
            else {

                //var jwt = _configuration.GetValue<string>("Jwt:Key");

                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                /*
                var tokenHandler = new JwtSecurityTokenHandler();
                var byteKey = Encoding.UTF8.GetBytes(jwt);
                DateTime expiration = DateTime.UtcNow.AddDays(1);
                var tokenDes = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.username,
                               ClaimTypes.Actor, res.rolname),
                    
                    }),
                    Expires = expiration,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                                SecurityAlgorithms.HmacSha256Signature
                                                                )
                };

                var token = tokenHandler.CreateToken(tokenDes);

                return new ResponseAuth { 
                                            usuario = user.username,
                                            token = tokenHandler.WriteToken(token),
                                            fechaExpiracion = expiration
                };*/

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", "a"),
                    new Claim("usuario", user.username)



                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signIn
                );

                return new ResponseAuth
                {
                    usuario = user.username,
                    fechaExpiracion= DateTime.Now.AddDays(1),
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                };


            }


        }
    }
}
