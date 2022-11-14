using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Universidade_Api;
using Pomelo.EntityFrameworkCore.MySql;

namespace Universidade_Api
{
    public class UniversidadeContext : DbContext
    {

        public UniversidadeContext(DbContextOptions<UniversidadeContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; } = null!;

        public DbSet<Curso> Cursos { get; set; } = null!;

        public DbSet<UnidadeCurricular> UnidadesCurriculares { get; set; } = null!;

        public DbSet<Nota> Notas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;Database=Universidade;user=root;password=root;", new MySqlServerVersion(new Version(8, 0, 11)));
        }


    }

}