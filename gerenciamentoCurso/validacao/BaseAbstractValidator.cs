using FluentValidation;

namespace gerenciamentoCurso.validacao
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T>
    {
        public IServiceProvider serviceProvider { get; }

        protected BaseAbstractValidator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}
