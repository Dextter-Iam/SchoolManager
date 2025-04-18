namespace MVCCamiloMentoria.Models
{
    public class AlunoResponsavel
    {
        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }

        public int ResponsavelId { get; set; }
        public Responsavel? Responsavel { get; set; }
    }
}
