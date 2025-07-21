using AutoMapper;
using gerenciamentoCurso.aplicacao.Dtos.commands.aluno;
using gerenciamentoCurso.aplicacao.Dtos.commands.turma;
using gerenciamentoCurso.aplicacao.Dtos.queries.aluno;
using gerenciamentoCurso.aplicacao.Dtos.queries.turma;
using gerenciamentoCurso.Dominio.commands.matricula;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.solicitar;

namespace gerenciamentoCurso.aplicacao.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FiltrarTurmaRequest, FiltrarTurmaQuery>().ReverseMap();
            CreateMap<FiltrarAlunoRequest, FiltrarAlunoQuery>().ReverseMap();
            CreateMap<FiltrarAlunoRequest, FiltrarAlunoQuery>().ReverseMap();
            CreateMap<CriarAlunoCommand, Aluno>().ReverseMap();
            CreateMap<CriarTurmaCommand, Turma>().ReverseMap();
            CreateMap<CriarMatriculaCommand, Matricula>().ReverseMap();
        }
    }
}