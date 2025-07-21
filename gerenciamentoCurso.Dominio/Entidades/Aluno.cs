using gerenciamentoCurso.Dominio.Entidades.Auth;

namespace gerenciamentoCurso.Dominio.Entidades
{
    public class Aluno : Usuario
    {
        public List<Matricula> Matriculas { get; set; }
    }
}
