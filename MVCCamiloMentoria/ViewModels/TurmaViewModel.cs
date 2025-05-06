using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class TurmaViewModel
    {
        [DisplayName("ID da Turma")]
        public int TurmaId { get; set; }

        [DisplayName("Nome da Turma")]
        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        public string? NomeTurma { get; set; }

        [DisplayName("Ano Letivo")]
        [Required(ErrorMessage = "O ano letivo é obrigatório.")]
        public int AnoLetivo { get; set; }

        [DisplayName("Turno")]
        [Required(ErrorMessage = "O turno é obrigatório.")]
        public string? Turno { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }
        [BindNever]
        public EscolaViewModel? Escola { get; set; }
        [DisplayName("Aulas")]
        public List<AulaViewModel> Aulas { get; set; } = new();

        [DisplayName("Alunos")]
        public List<AlunoViewModel>? Alunos { get; set; } = new();

        [DisplayName("Professores")]
        public List<ProfessorTurmaViewModel> Professores { get; set; } = new();

        [DisplayName("Disciplinas da Turma")]
        public ICollection<TurmaDisciplinaViewModel> TurmaDisciplinas { get; set; } = new List<TurmaDisciplinaViewModel>();
    }
}
