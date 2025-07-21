using gerenciamentoCurso.api.validacao.usuario;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.commands.usuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamentoCurso.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Adm")]
    public class UsuarioController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CriarUsuarioValidator _validador;
        private readonly AtualizarUsuarioValidador _atualizarValidador;
        private readonly DeletarUsuarioValidador _deletarValidador;
        private readonly AlterarSenhaValidador _senhaValidador;

        public UsuarioController(IMediator mediator, CriarUsuarioValidator validador, AtualizarUsuarioValidador atualizarValidador, DeletarUsuarioValidador deletarValidador, AlterarSenhaValidador senhaValidador)
        {
            _mediator = mediator;
            _validador = validador;
            _atualizarValidador = atualizarValidador;
            _deletarValidador = deletarValidador;
            _senhaValidador = senhaValidador;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand criarMatricula)
        {
            var resultResultado = _validador.Validate(criarMatricula);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            var x = await _mediator.Send(criarMatricula);
            return Created("", x);
        }

        [HttpPut()]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioCommand cancelarMatricula)
        {
            var resultResultado = _atualizarValidador.Validate(cancelarMatricula);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Ok(await _mediator.Send(cancelarMatricula));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeletarUsuario([FromBody] DeletarUsuarioCommand turma)
        {
            var resultResultado = _deletarValidador.Validate(turma);

            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return NoContent();
        }

        [HttpPatch()]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaCommand senha)
        {
            var resultResultado = _senhaValidador.Validate(senha);
            if (!resultResultado.IsValid)
            {
                return BadRequest(resultResultado.Errors);
            }
            return Ok(await _mediator.Send(senha));
        }

    }
}
