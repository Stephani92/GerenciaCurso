using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.aluno
{
    public class DeletarAlunoHandler : IRequestHandler<DeletarUsuarioCommand, bool>
    {
        private readonly IAlunoRepositorio _alunoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarAlunoHandler(IAlunoRepositorio alunoRepository, IUnitOfWork unitOfWork)
        {
            _alunoRepository = alunoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.GetAlunoAsyncByEmailCpf(request.Email, request.Cpf);
            aluno.Ativo = false;

            _alunoRepository.Atualizar(aluno);            

            return await _unitOfWork.CommitAsync();
        }
    }
}
