using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.usuario
{
    public class DeletarUsuarioHandler : IRequestHandler<DeletarUsuarioCommand, bool>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarUsuarioHandler(IUsuarioRepositorio usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepositorio = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var turma = await _usuarioRepositorio.GetUsuarioAsyncByEmailCpf(request.Email, request.Cpf);
            turma.Ativo = false;

            _usuarioRepositorio.Atualizar(turma);

            return await _unitOfWork.CommitAsync();
        }
    }
}
