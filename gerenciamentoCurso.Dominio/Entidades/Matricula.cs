namespace gerenciamentoCurso.Dominio.Entidades
{
    public class Matricula
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public Guid TurmaId { get; set; }
        public Turma Turma { get; set; }

        public DateTime DataMatricula { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; }
    }
}
