using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class ProfessorTurmaViewModel
    {
        public int ProfessorTurmaId { get; set; }
        public int TurmaId { get; set; }
        public TurmaViewModel? Turma { get; set; }
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }
    }
}
