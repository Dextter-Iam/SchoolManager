using MVCCamiloMentoria.Models;

public class Professor
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Matricula { get; set; }
    public byte[]? Foto { get; set; }
    public int EscolaId { get; set; }
    public Escola? Escola { get; set; }

    public int EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }

    public List<Telefone>? Telefones { get; set; }
    public List<Aula>? Aulas { get; set; }
    public List<ProfessorTurma>? Turmas { get; set; }
    public List<ProfessorDisciplina>? Disciplinas { get; set; }

    public bool Excluido { get; set; } = false; // << Adicionado aqui!
}
