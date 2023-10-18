using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamingManagement.Models.dto.Response;
using StreamingManagement.Utils.PSS;

namespace StreamingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult getAllServices()
        {
            try
            {
                return Ok(new Response(200, "ok", new ManageOperation().getService()));

            }
            catch (Exception ex)
            {
                return BadRequest(new Response(400, "error al ingresar " + ex.Message, null));
            }
        }

    }
}
