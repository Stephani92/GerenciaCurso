using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio.Repositorio
{
    public class MatriculaRepositorio : DefaultRepository, IMatriculaRepositorio
    {
        public MatriculaRepositorio(AppDbContext _data) : base(_data)
        {
        }

        public async Task<bool> AnyMatriculaAsyncByAlunoIdETurmaId(Guid alunoId, Guid turmaId)
        {
            return await data.Matriculas.AnyAsync(m => m.Aluno.Id == alunoId && m.Turma.Id == turmaId);
        }

        public async Task CancelarMatricula(Guid alunoId, Guid turmaId)
        {
            var matricula = await data.Matriculas.FirstOrDefaultAsync(m => m.Aluno.Id == alunoId && m.Turma.Id == turmaId);
            matricula.Ativo = false;
            data.Matriculas.Update(matricula);
        }
    }
}
