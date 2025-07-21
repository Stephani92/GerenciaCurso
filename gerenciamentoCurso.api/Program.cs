using System.Security.Claims;
using System.Text;
using gerenciamentoCurso.api.validacao;
using gerenciamentoCurso.api.validacao.aluno;
using gerenciamentoCurso.api.validacao.matricula;
using gerenciamentoCurso.api.validacao.turma;
using gerenciamentoCurso.api.validacao.usuario;
using gerenciamentoCurso.aplicacao.CasosDeUso;
using gerenciamentoCurso.aplicacao.CasosDeUso.aluno;
using gerenciamentoCurso.aplicacao.CasosDeUso.matricula;
using gerenciamentoCurso.aplicacao.CasosDeUso.turma;
using gerenciamentoCurso.aplicacao.CasosDeUso.usuario;
using gerenciamentoCurso.aplicacao.Mappings;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using gerenciamentoCurso.Dominio.Interfaces;
using gerenciamentoCurso.Dominio.Interfaces.repositorios;
using gerenciamentoCurso.repositorio;
using gerenciamentoCurso.repositorio.Repositorio;
using gerenciamentoCurso.Servico.Servicos;
using gerenciamentoCurso.validacao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
        )
    );

builder.Services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
builder.Services.AddScoped<ITurmaRepositorio, TurmaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IMatriculaRepositorio, MatriculaRepositorio>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<AtualizarAlunoValidador>();
builder.Services.AddScoped<CriarAlunoValidator>();
builder.Services.AddScoped<DeletarAlunoValidador>();
builder.Services.AddScoped<FiltroAlunoValidador>();


builder.Services.AddScoped<AtualizarUsuarioValidador>();
builder.Services.AddScoped<CriarUsuarioValidator>();
builder.Services.AddScoped<DeletarUsuarioValidador>();
builder.Services.AddScoped<FiltrarUsuarioValidador>();
builder.Services.AddScoped<AlterarSenhaValidador>();


builder.Services.AddScoped<TurmaValidator>();
builder.Services.AddScoped<DeletarTurmaValidador>();
builder.Services.AddScoped<FiltroTurmaValidador>();


builder.Services.AddScoped<LoginValidador>();
builder.Services.AddScoped<CancelarMatriculaValidador>();
builder.Services.AddScoped<CriarMatriculaValidador>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(LoginHandler).Assembly

        , typeof(CriarAlunoHandler).Assembly
        , typeof(AtualizarAlunoHandler).Assembly
        , typeof(DeletarAlunoHandler).Assembly
        , typeof(FiltraAlunoHandler).Assembly

        , typeof(CriarTurmaHandler).Assembly
        , typeof(AtualizarTurmaHandler).Assembly
        , typeof(DeletarTurmaHandler).Assembly
        , typeof(FiltrarTurmaHandler).Assembly

        , typeof(CriarUsuarioHandler).Assembly
        , typeof(AtualizarUsuarioHandler).Assembly
        , typeof(DeletarUsuarioHandler).Assembly
        , typeof(FiltrarUsuarioHandler).Assembly

        , typeof(CriarMatriculaHandler).Assembly
        , typeof(CancelarMatriculaHandler).Assembly
         );
});

builder.Services.AddScoped<IJwtService, JwtService>();


JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Newtonsoft.Json.Formatting.Indented,
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(jwtSettings.Secret))
            ,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe o token JWT no formato: Bearer {seu token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddControllers();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


