namespace MVCCamiloMentoria.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Professor>? Professores { get; set; } 
        public List<Turma>?Turma { get; set;} 
        public List<Aula>? Aula { get; set; } 
    }
}
