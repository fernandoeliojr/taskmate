using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace TaskMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Este endpoint é público, qualquer um pode acessar.");
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("🎉 Você acessou um endpoint protegido com JWT!");
        }
    }
}