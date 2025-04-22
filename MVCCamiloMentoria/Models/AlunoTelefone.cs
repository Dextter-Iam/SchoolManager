namespace MVCCamiloMentoria.Models
{
    public class AlunoTelefone
    {
        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }

        public int TelefoneId { get; set; }
        public Telefone? Telefone { get; set; }

        public int DDD { get; set; }
        public int Numero { get; set; }
    }
}
