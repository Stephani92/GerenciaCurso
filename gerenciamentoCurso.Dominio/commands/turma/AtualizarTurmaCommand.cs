using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.commands.turma
{
    public class AtualizarTurmaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
