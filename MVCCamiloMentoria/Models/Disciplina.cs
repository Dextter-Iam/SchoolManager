namespace MVCCamiloMentoria.Models
{
    public class Disciplina
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public List<Professor> Professores { get; set; } = new List<Professor>();

    }
}
