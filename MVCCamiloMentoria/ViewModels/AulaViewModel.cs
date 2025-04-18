using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    [NotMapped]
    public class AulaViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFim { get; set; }
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina? Disciplina { get; set; }
        public bool ConfirmacaoPresenca { get; set; }
        public List<Aluno>? AlunosPresentes { get; set; }

    }
}
