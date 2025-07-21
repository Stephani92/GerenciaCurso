namespace gerenciamentoCurso.Dominio.Entidades.Resposta
{
    public class UsuariosResponse
    {
        public List<AlunoResponse> Usuario { get; set; }
        public PaginacaoResponse Paginacao { get; set; }
    }
}
