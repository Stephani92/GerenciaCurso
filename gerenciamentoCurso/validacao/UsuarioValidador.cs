using FluentValidation;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace gerenciamentoCurso.validacao
{
    public class UsuarioValidator : BaseAbstractValidator<Usuario>
    {
       
        public UsuarioValidator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IUsuarioRepository>();

            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage("O nome não pode ser vazio.")
                                            .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage("O email não pode ser vazio.")
                                             .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(usuario => usuario).Must( usuario => service.AnyUsuarioAsyncByEmail(usuario.Email).Result).WithMessage("Usuario não encontrado");

        }
    }
}
