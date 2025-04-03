namespace MVCCamiloMentoria.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public int Matricula {  get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Aula>? Aulas { get; set; } = new List<Aula>();
        public List<ProfessorTurma>? Turmas { get; internal set; }
        public List<Disciplina>? Disciplinas { get; internal set; } = new List<Disciplina>();
    }

}
