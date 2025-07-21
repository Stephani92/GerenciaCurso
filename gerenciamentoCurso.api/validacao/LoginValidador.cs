using FluentValidation;
using gerenciamentoCurso.api.validacao.@base;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;

namespace gerenciamentoCurso.api.validacao
{
    public class LoginValidador : BaseAbstractValidator<string>
    {

        public LoginValidador(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IAlunoRepositorio>();

            

            RuleFor(usuario => usuario).NotEmpty().WithMessage("O email não pode ser vazio.")
                                             .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(usuario => usuario).Must(usuario => service.AnyUsuarioAsyncByEmail(usuario).Result).WithMessage("Usuario não encontrado");

        }
    }
}