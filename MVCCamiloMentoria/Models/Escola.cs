namespace MVCCamiloMentoria.Models
{
    public class Escola
    {
        public int EscolaId { get; set; }

        public string Nome { get; set; }

        public int EnderecoId { get; set; }

        public Endereco? Endereco { get; set; }

        public List<Turma>? Turmas { get; set; } = new List<Turma>();
    }
}
