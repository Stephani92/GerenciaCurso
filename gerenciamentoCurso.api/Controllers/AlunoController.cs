using gerenciamentoCurso.api.validacao.aluno;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.aplicacao.Dtos.queries.aluno;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Adm")]
    public class AlunoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CriarAlunoValidator _validador;
        private readonly AtualizarAlunoValidador _atualizarValidador;
        private readonly DeletarAlunoValidador _deletarValidador;
        private readonly FiltroAlunoValidador _filtrarAlunoValidador;

        public AlunoController(IMediator mediator, CriarAlunoValidator validador, AtualizarAlunoValidador atualizarValidador, DeletarAlunoValidador deletarValidador, FiltroAlunoValidador filtrarAlunoValidador)
        {
            _mediator = mediator;
            _validador = validador;
            _atualizarValidador = atualizarValidador;
            _deletarValidador = deletarValidador;
            _filtrarAlunoValidador = filtrarAlunoValidador;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarAluno([FromBody] CriarAlunoCommand usuario)
        {
            var resultResultado = _validador.Validate(usuario);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Created("", await _mediator.Send(usuario));
        }

        [HttpPatch()]
        public async Task<IActionResult> AtualizarAluno([FromBody] AtualizarAlunoCommand usuario)
        {
            var resultResultado = _atualizarValidador.Validate(usuario);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Ok(await _mediator.Send(usuario));
        }

        [HttpGet()]
        public async Task<IActionResult> BuscarAluno([FromQuery] FiltrarAlunoQuery usuario)
        {   
            return Ok(await _mediator.Send(usuario));
        }
        [HttpDelete()]
        public async Task<IActionResult> DeletarAluno([FromBody] DeletarUsuarioCommand alunoDeletar)
        {
            var resultResultado = _deletarValidador.Validate(alunoDeletar);

            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            
            return Ok(await _mediator.Send(alunoDeletar));
        }
    }
}
