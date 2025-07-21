using gerenciamentoCurso.aplicacao.Dtos.solicitar;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using MediatR;

namespace gerenciamentoCurso.aplicacao.Dtos.queries.aluno
{
    public class FiltrarAlunoQuery :  IRequest<AlunosResponse>
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Cpf { get; set; }
        public bool Ativo { get; set; } = true;
        public PaginacaoRequest Paginacao { get; set; } = new PaginacaoRequest();
    }
}
