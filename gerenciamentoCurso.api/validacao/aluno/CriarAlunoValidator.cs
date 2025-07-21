
using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.aluno
{
    public class CriarAlunoValidator : BaseAbstractValidator<CriarAlunoCommand>
    {
       
        public CriarAlunoValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            RuleFor(usuario => usuario.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter ao menos um caractere especial.");

            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome não pode ser vazio.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(usuario => usuario.Sobrenome).NotEmpty().WithMessage("O nome não pode ser vazio.")
                                            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O email não pode ser vazio.")
                                             .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(usuario => usuario)
                .Must( usuario => !service.AnyUsuarioAsyncByEmailCpf(usuario.Email, usuario.Cpf).Result).WithMessage("Usuario não encontrado");

        }
    }
}
