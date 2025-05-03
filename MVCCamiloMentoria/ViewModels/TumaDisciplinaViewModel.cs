using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class TurmaDisciplinaViewModel
{
    public int TurmaId { get; set; }
    public int DisciplinaId { get; set; }
    public Turma? Turma { get; set; }
    public Disciplina? Disciplina { get; set; }
}
