namespace gerenciamentoCurso.Dominio.Entidades.Resposta
{
    public class TurmasResponse
    {
        public List<TurmaResponse> Turma { get; set; }
        public PaginacaoResponse Paginacao { get; set; }
    }
    public class TurmaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public dynamic Alunos { get; set; }
        public bool Ativo { get; set; }
    }
}
