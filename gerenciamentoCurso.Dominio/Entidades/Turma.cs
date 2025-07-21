namespace gerenciamentoCurso.Dominio.Entidades
{
    public class Turma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; }

        public List<Matricula> Matriculas { get; set; } = new();
    }
}
