using gerenciamentoCurso.aplicacao.Dtos.solicitar;

namespace gerenciamentoCurso.Dominio.Entidades.solicitar
{
    public class FiltrarAlunoRequest 
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; } = true;

        public PaginacaoRequest Paginacao { get; set; }
    }
}
