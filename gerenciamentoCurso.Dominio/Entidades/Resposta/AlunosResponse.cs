namespace gerenciamentoCurso.Dominio.Entidades.Resposta
{
    public class AlunosResponse
    {
        public List<AlunoResponse> Alunos { get; set; }
        public PaginacaoResponse Paginacao { get; set; }
    }
    public class AlunoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
        public dynamic Turmas { get; set; }
    }
}
