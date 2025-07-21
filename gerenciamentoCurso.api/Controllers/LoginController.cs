using gerenciamentoCurso.api.validacao;
using gerenciamentoCurso.aplicacao.Dtos.commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly LoginValidador _validador;
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator, LoginValidador validador)
        {
            _validador = validador;
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            var resultValidador = _validador.Validate(command.Email);
            if (resultValidador.IsValid)
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token });
            }
            return BadRequest(resultValidador.Errors);

        }
    }
}
