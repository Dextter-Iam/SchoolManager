namespace MVCCamiloMentoria.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Professor> Professores { get; set; } = new List<Professor>();
        public List<Turma>Turmas { get; set;} = new List<Turma>();
        public List<Aula> Aulas { get; set; } = new List<Aula> ();
    }
}
