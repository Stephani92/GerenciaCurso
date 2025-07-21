using gerenciamentoCurso.aplicacao.Dtos.solicitar;

namespace gerenciamentoCurso.Dominio.Entidades.solicitar
{
    public class FiltrarUsuarioRequest 
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }

        public PaginacaoRequest Paginacao { get; set; }
    }
}
