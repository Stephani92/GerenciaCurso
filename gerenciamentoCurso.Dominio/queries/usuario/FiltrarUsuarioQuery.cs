using gerenciamentoCurso.aplicacao.Dtos.solicitar;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using MediatR;

namespace gerenciamentoCurso.Dominio.queries.usuario
{
    public class FiltrarUsuarioQuery :  IRequest<UsuariosResponse>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public PaginacaoRequest Paginacao { get; set; }
    }
}