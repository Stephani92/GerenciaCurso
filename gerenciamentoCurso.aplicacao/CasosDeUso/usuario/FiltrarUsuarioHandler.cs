using AutoMapper;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.Dominio.queries.usuario;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.usuario
{
    public class FiltrarUsuarioHandler : IRequestHandler<FiltrarUsuarioQuery, UsuariosResponse>
    {
        private readonly IUsuarioRepositorio _usuarioRepository;

        public FiltrarUsuarioHandler(IUsuarioRepositorio usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuariosResponse> Handle(FiltrarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuarioFiltro = new FiltrarUsuarioRequest()
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Cpf = request.Cpf,
                Ativo = request.Ativo,
                Paginacao = request.Paginacao
            };
            return await _usuarioRepository.FiltrarAsync(usuarioFiltro);
        }
    }
}
