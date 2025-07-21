using System.Data;
using gerenciamentoCurso.Dominio.Entidades;
using gerenciamentoCurso.Dominio.Entidades.Auth;
using Microsoft.EntityFrameworkCore;

namespace gerenciamentoCurso.repositorio
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Usuario>("Usuario")
                .HasValue<Aluno>("Aluno");

            modelBuilder.Entity<Roles>()
                .HasKey(r => r.Nome);


            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.UsuarioId, ur.RoleNome });

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ur => ur.UsuarioId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleNome);


            modelBuilder.Entity<Turma>()
                .Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);


            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId);
        }
    }
}