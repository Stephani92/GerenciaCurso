using gerenciamentoCurso.api.validacao;
using gerenciamentoCurso.api.validacao.turma;
using gerenciamentoCurso.aplicacao.Dtos.commands.turma;
using gerenciamentoCurso.aplicacao.Dtos.queries.turma;
using gerenciamentoCurso.validacao;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Adm")]
    public class TurmaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly TurmaValidator _validador;
        private readonly DeletarTurmaValidador _turmaValidador;
        private readonly FiltroTurmaValidador _filtroValidador;

        public TurmaController(IMediator mediator, TurmaValidator validador, DeletarTurmaValidador turmaValidador, FiltroTurmaValidador filtroValidador)
        {
            _mediator = mediator;
            _validador = validador;
            _turmaValidador = turmaValidador;
            _filtroValidador = filtroValidador;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarTurma([FromBody] CriarTurmaCommand turma)
        {
            var resultResultado = _validador.Validate(turma.Nome);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Created("", await _mediator.Send(turma));
        }
        [HttpPut()]
        public async Task<IActionResult> AtualizarTurma([FromBody] AtualizarTurmaCommand turma)
        {
            var resultResultado = _validador.Validate(turma.Nome);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Ok(await _mediator.Send(turma));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeletarTurma([FromBody] DeletarTurmaCommand turma)
        {
            var resultResultado = _turmaValidador.Validate(turma);

            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            await _mediator.Send(turma);

            return NoContent();
        }

        [HttpGet()]
        public async Task<IActionResult> BuscarTurma([FromQuery] FiltrarTurmaQuery turma)
        {
            var resuiltResultado = _filtroValidador.Validate(turma);
            if (!resuiltResultado.IsValid)
            {
                return BadRequest(resuiltResultado.Errors);
            }
            return Ok(await _mediator.Send(turma));
        }
    }
}
