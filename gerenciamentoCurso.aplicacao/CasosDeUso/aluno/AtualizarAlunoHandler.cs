using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using MediatR;

namespace gerenciamentoCurso.aplicacao.CasosDeUso.aluno
{
    public class AtualizarAlunoHandler : IRequestHandler<AtualizarAlunoCommand, bool>
    {
        private readonly IUsuarioRepositorio _alunoRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarAlunoHandler(IUsuarioRepositorio alunoRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _alunoRepository = alunoRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AtualizarAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.GetUsuarioAsyncByEmailCpf(request.Email, request.Cpf);

            var alunoAtualizado = new Aluno()
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                DataNascimento = request.DataNascimento
            };

            _alunoRepository.Atualizar(aluno);
            return  await _unitOfWork.CommitAsync();
        }
    }
}
