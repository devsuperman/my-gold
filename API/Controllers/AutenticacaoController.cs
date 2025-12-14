using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/autenticacao")]
    public partial class AutenticacaoController(AutenticacaoService autenticacaoService) : ControllerBase
    {
        private readonly AutenticacaoService _autenticacaoService = autenticacaoService;

        [HttpGet]
        public IActionResult Get() => Ok($"Olá {User.Identity.Name}");

        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest model)
        {
            var response = await _autenticacaoService.LoginAsync(model.password);

            if (response.Success)
                return Ok(response);

            return Ok(response);
        }

        public record LoginRequest(string password);
    }
}
