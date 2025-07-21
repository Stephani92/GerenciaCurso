using gerenciamentoCurso.aplicacao.Dtos.solicitar;

namespace gerenciamentoCurso.Dominio.Entidades.solicitar
{
    public class FiltrarTurmaRequest
    {
        public string? Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool Ativo { get; set; }

        public PaginacaoRequest Paginacao { get; set; }
    }
}
