using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StreamingManagement.Models.dto.Authorization;
using StreamingManagement.Models.dto.Response;
using StreamingManagement.Utils.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StreamingManagement.Controllers.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public IConfiguration _configuration;
        private Authenticator _autenticador;
        public AuthorizationController(IConfiguration config) { 
            _configuration = config;
            _autenticador = new Authenticator();
        }


        [HttpPost]
        public IActionResult validEnter(User user) {

            try {
                if(!ModelState.IsValid)
                {
                    return BadRequest( new Response(400,
                                                    "Campos requeridos",
                                                    ModelState.Values
                                                    .SelectMany( v => v.Errors)
                                                    .Select(e => e.ErrorMessage)
                                                    .ToList()

                                                    ));

                }
                
                return Ok(new Response(200,"OK", _autenticador.validarInicio(user)));
            
            }catch (Exception ex)
            {

                return BadRequest( new Response(400,ex.Message, null));
            }


        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("¡Hola, mundo!");
        }
    }
}
