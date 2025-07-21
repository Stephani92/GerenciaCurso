using FluentValidation;

namespace gerenciamentoCurso.api.validacao.@base
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T>
    {
        public CascadeMode CascadeMode { get; }
        public IServiceProvider serviceProvider { get; }

        protected BaseAbstractValidator(IServiceProvider serviceProvider)
        {
            CascadeMode = CascadeMode.Stop;
            this.serviceProvider = serviceProvider;
        }
    }
}
