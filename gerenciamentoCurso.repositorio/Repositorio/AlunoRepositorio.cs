using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio.Repositorio
{
    public class AlunoRepositorio : DefaultRepository, IAlunoRepositorio
    {
        public AlunoRepositorio(AppDbContext _data) : base(_data) { }
        public async Task<Aluno> GetUsuarioAsyncByEmailLogin(string UserEmail)
        {
            return await data.Alunos
             .Include(c => c.UserRole)
             .ThenInclude(c => c.Role).Where(c => c.Email == UserEmail).FirstOrDefaultAsync();
        }
        public async Task<Aluno> GetAlunoAsyncByEmailCpf(string UserEmail, string cpf)
        {
            var query = await data.Alunos
                .Include(c => c.Matriculas)
                .ThenInclude(c => c.Turma)
                .Where(c => c.Email == UserEmail && c.Cpf == cpf).FirstOrDefaultAsync();
            return query;
        }

        public async Task<bool> AnyUsuarioAsyncByEmail(string UserEmail)
        {
            return await data.Usuarios.AnyAsync(c => c.Email == UserEmail);
        }
        async Task<bool> IAlunoRepositorio.AnyUsuarioAsyncByEmailCpf(string UserEmail, string cpf)
        {
            return await data.Alunos.AnyAsync(c => c.Email == UserEmail && c.Cpf == cpf);
        }

        public async Task<AlunosResponse> FiltrarAsync(FiltrarAlunoRequest filtro)
        {
            var query = data.Alunos.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                query = query.Where(c => c.Nome.Contains(filtro.Nome));
            }

            if (!string.IsNullOrEmpty(filtro.Sobrenome))
            {
                query = query.Where(c => c.Sobrenome.Contains(filtro.Sobrenome));
            }

            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                query = query.Where(c => c.Cpf.Contains(filtro.Cpf));
            }

            query = query.Where(c => c.Ativo == filtro.Ativo); 

            var total = await query.CountAsync();

            query =  query
                .OrderBy(x => x.Nome)
                .Skip((filtro.Paginacao.Page - 1) * filtro.Paginacao.PageSize)
                .Take(filtro.Paginacao.PageSize);

            var alunosResponse = new AlunosResponse();
            alunosResponse.Alunos = query.Select(c => new AlunoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Ativo = c.Ativo,
                Cpf = c.Cpf,
                Email = c.Email,
                DataNascimento = c.DataNascimento,
                Turmas = c.Matriculas.Select(m => new
                {
                    TurmaId = m.Turma.Id,
                    TurmaNome = m.Turma.Nome,
                    m.DataMatricula
                }).ToList()
            }).OrderBy(x => x.Nome).ToList();

            alunosResponse.Paginacao = new PaginacaoResponse
            {
                PaginaAtual = filtro.Paginacao.Page,
                TamanhoPagina = filtro.Paginacao.PageSize,
                TotalItens = total
            };
            return alunosResponse;
        }


        public async Task<bool> AnyAlunoAsyncById(Guid Id)
        {
            return await data.Alunos.AnyAsync(x => x.Id == Id);
        }
        public async Task AddRoles(Guid UserEmail)
        {
            await data.AddAsync(new UserRoles(UserEmail, "Aluno"));
        }
        
    }
}
