using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.aplicacao.Dtos.commands.usuario;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao.usuario
{
    public class AtualizarUsuarioValidador : BaseAbstractValidator<AtualizarUsuarioCommand>
    {

        public AtualizarUsuarioValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IUsuarioRepositorio>();


            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome não pode ser vazio.")
                                            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome não pode ser vazio.")
                                            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(x => x.Cpf).CpfValido().WithMessage("O Cpf deve ser válido.");

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O email não pode ser vazio.")
                                             .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(usuario => usuario)
                .Must(usuario => service.AnyUsuarioAsyncByEmailCpf(usuario.Email, usuario.Cpf).Result).WithMessage("Usuario não encontrado");
        }
    }
}
