namespace MVCCamiloMentoria.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public int Matricula {  get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Aula>? Aulas { get; set; }
        public List<ProfessorTurma>? Turmas { get; internal set; }
        public List<Disciplina>? Disciplinas { get; internal set; }
    }

}
