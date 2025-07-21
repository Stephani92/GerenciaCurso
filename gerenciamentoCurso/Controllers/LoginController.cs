using gerenciamentoCurso.aplicacao.Dtos;
using gerenciamentoCurso.Dominio.Entidades.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {

            var token = await _mediator.Send(command);
            return Ok(new { Token = token });

        }
    }
}
