
namespace Universidade_Api
{
    public class Aluno
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public Curso? Curso { get; set; }
        public Double? Saldo { get; set; }
        public string? Email { get; set; }
        public ICollection<UnidadeCurricular>? UnidadesCurriculares { get; set; }
    }
}