namespace Universidade_Api
{
    public class AlunoDTO
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? SiglaCurso { get; set; }
        public long? Saldo { get; set; }
        public string? Email { get; set; }
        public ICollection<string?>? SiglasUcs { get; set; }
    }
}
