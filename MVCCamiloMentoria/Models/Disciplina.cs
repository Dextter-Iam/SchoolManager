namespace MVCCamiloMentoria.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Professor>? Professores { get; set; } 
        public List<Turma>?Turmas { get; set;} 
        public List<Aula>? Aulas { get; set; } 
    }
}
