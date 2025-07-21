using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Entidades.Resposta;
using gerenciamentoCurso.Dominio.Entidades.solicitar;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio.Repositorio
{
    public class UsuarioRepositorio : DefaultRepository, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AppDbContext _data) : base(_data)
        {
        }

        public async Task<bool> AnyUsuarioAsyncByEmailCpf(string UserEmail, string cpf)
        {
            return await data.Usuarios.AnyAsync(c => c.Email == UserEmail && c.Cpf == cpf);
        }

        public async Task<UsuariosResponse> FiltrarAsync(FiltrarUsuarioRequest filtro)
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

            query = query
                .OrderBy(x => x.Nome)
                .Skip((filtro.Paginacao.Page - 1) * filtro.Paginacao.PageSize)
                .Take(filtro.Paginacao.PageSize);

            var alunosResponse = new UsuariosResponse();
            alunosResponse.Usuario = await query.Select(c => new AlunoResponse
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
            }).OrderBy(x => x.Nome).ToListAsync();

            alunosResponse.Paginacao = new PaginacaoResponse
            {
                PaginaAtual = filtro.Paginacao.Page,
                TamanhoPagina = filtro.Paginacao.PageSize,
                TotalItens = total
            };
            return alunosResponse;
        }

        public async Task<Usuario> GetUsuarioAsyncByEmailCpf(string UserEmail, string cpf)
        {
            var query = await data.Usuarios.Where(c => c.Email == UserEmail && c.Cpf == cpf).FirstOrDefaultAsync();
            return query;
        }
        public  async Task AddRoles(Guid UserEmail)
        {
            await data.AddAsync(new UserRoles(UserEmail, "Adm"));
        }
        public async Task<Usuario> GetUsuarioAsyncByEmailLogin(string UserEmail)
        {
            return await data.Usuarios
             .Include(c => c.UserRole)
             .ThenInclude(c => c.Role).Where(c => c.Email == UserEmail).FirstOrDefaultAsync();
        }
    }
}
