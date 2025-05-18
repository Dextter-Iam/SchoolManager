using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;


[NotMapped]
public class TurmaDisciplinaViewModel
{
    public int TurmaId { get; set; }
    public int DisciplinaId { get; set; }
    public TurmaViewModel? Turma { get; set; }
    public DisciplinaViewModel? Disciplina { get; set; }
}
