namespace gerenciamentoCurso.Dominio.Entidades.Resposta
{
    public class PaginacaoResponse
    {
        public int TotalItens { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / TamanhoPagina);
    }
}
