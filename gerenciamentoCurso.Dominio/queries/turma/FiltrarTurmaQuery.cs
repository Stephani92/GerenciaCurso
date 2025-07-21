using gerenciamentoCurso.aplicacao.Dtos.solicitar;
using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.queries.turma
{
    public class FiltrarTurmaQuery : IRequest<object>
    {
        public string? Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool Ativo { get; set; } = true;

        public PaginacaoRequest Paginacao { get; set; }
    }
}
