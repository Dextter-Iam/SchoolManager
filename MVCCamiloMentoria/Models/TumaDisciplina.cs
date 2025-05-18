using MVCCamiloMentoria.Models;

public class TurmaDisciplina
{
    public int TurmaId { get; set; }
    public int DisciplinaId { get; set; }
    public Turma? Turma { get; set; }
    public Disciplina? Disciplina { get; set; }

    public bool Excluido { get; set; } = false;
}
