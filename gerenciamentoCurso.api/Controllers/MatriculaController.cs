using gerenciamentoCurso.api.validacao.matricula;
using gerenciamentoCurso.Dominio.commands.matricula;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Adm")]
    public class MatriculaController : Controller
    {
        private readonly CriarMatriculaValidador _validador;
        private readonly IMediator _mediator;
        private readonly CancelarMatriculaValidador _cancelarValidador;

        public MatriculaController(IMediator mediator, CriarMatriculaValidador validador, CancelarMatriculaValidador cancelarValidador)
        {
            _validador = validador;
            _mediator = mediator;
            _cancelarValidador = cancelarValidador;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarMatricula([FromBody] CriarMatriculaCommand usuario)
        {
            var resultResultado = _validador.Validate(usuario);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Created("", await _mediator.Send(usuario));
        }

        [HttpPut("CancelarMatricula")]
        public async Task<IActionResult> CancelarMatricula([FromBody] CancelarMatriculaCommand usuario)
        {
            var resultResultado = _cancelarValidador.Validate(usuario);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Created("", await _mediator.Send(usuario));
        }
    }
}
