using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio.Repositorio
{
    public class TurmaRepositorio : DefaultRepository, ITurmaRepositorio
    {
        public TurmaRepositorio(AppDbContext _data) : base(_data)
        {
        }

        public Task<bool> AnyTurmaAsyncById(Guid id)
        {
            return data.Turmas.AnyAsync(t => t.Id == id);
        }

        

        public async Task<TurmasResponse> FiltrarAsync(FiltrarTurmaRequest filtro)
        {
            var query = data.Turmas.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                query = query.Where(c => c.Nome.Contains(filtro.Nome));
            }

            if (filtro.DataInicio.HasValue)
                query = query.Where(c => c.DataCriacao >= filtro.DataInicio.Value);

            if (filtro.DataFinal.HasValue)
                query = query.Where(c => c.DataCriacao <= filtro.DataFinal.Value);

            query = query.Where(c => c.Ativo == filtro.Ativo);
            query = query.Where(c => c.Ativo == filtro.Ativo);

            var total = await query.CountAsync();

            query = query
                .OrderBy(x => x.Nome)
                .Skip((filtro.Paginacao.Page - 1) * filtro.Paginacao.PageSize)
                .Take(filtro.Paginacao.PageSize);

            var turmasResponse = new TurmasResponse();
            turmasResponse.Turma = query.Select(c => new TurmaResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                DataCriacao = c.DataCriacao,
                Ativo = c.Ativo,
                Alunos = c.Matriculas.Select(m => new
                {
                    AlunoId = m.Aluno.Id,
                    AlunoNome = m.Aluno.Nome,
                    m.DataMatricula
                }).ToList()
            }).OrderBy(x => x.Nome).ToList();

            turmasResponse.Paginacao = new PaginacaoResponse
            {
                PaginaAtual = filtro.Paginacao.Page,
                TamanhoPagina = filtro.Paginacao.PageSize,
                TotalItens = total
            };
            return turmasResponse;
        }

        public Task<Turma> GetTurmaAsyncById(Guid id)
        {
            return data.Turmas
                .Include(t => t.Matriculas)
                .ThenInclude(m => m.Aluno)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<List<Turma>> GetTurmaAsyncTodos()
        {
            return data.Turmas
                .Include(t => t.Matriculas)
                .ThenInclude(m => m.Aluno)
                .ToListAsync();
        }
        public async Task<bool> AnyAlunoTurmaAsyncById(Guid Id)
        {
            return await data.Matriculas.AnyAsync(x => x.AlunoId == Id);
        }
    }
}
