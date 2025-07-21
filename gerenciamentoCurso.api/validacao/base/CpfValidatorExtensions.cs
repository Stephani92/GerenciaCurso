using FluentValidation;

public static class CpfValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> CpfValido<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Must(ValidarCpf).WithMessage("CPF inválido.");
    }

    private static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var temp = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(temp[i].ToString()) * mult1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        temp += resto;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(temp[i].ToString()) * mult2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(resto.ToString());
    }
}
