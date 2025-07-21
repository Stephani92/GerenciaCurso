using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.aluno
{
    public class AtualizarAlunoValidador : BaseAbstractValidator<AtualizarAlunoCommand>
    {

        public AtualizarAlunoValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(usuario => usuario.Nome)
                                            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");


            RuleFor(usuario => usuario)
                .Must(usuario => service.AnyUsuarioAsyncByEmailCpf(usuario.Email, usuario.Cpf).Result).WithMessage("Usuario não encontrado");
        }
    }
}
