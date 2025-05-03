using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class AulaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Aula")]
        [Required(ErrorMessage = "O nome da aula é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Display(Name = "Horário de Início")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        public DateTime HorarioInicio { get; set; }

        [Display(Name = "Horário de Fim")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "O horário de fim é obrigatório.")]
        public DateTime HorarioFim { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

        [Display(Name = "Professor")]
        [Required(ErrorMessage = "O professor é obrigatório.")]

        public int ProfessorId { get; set; }
        public ProfessorViewModel? Professor { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "A turma é obrigatória.")]
        public int TurmaId { get; set; }
        public TurmaViewModel? Turma { get; set; }

        [Display(Name = "Disciplina")]
        [Required(ErrorMessage = "A disciplina é obrigatória.")]
        public int DisciplinaId { get; set; }
        public DisciplinaViewModel? Disciplina { get; set; }

        [Display(Name = "Confirmação de Presença")]
        public bool ConfirmacaoPresenca { get; set; }

        [Display(Name = "Alunos Presentes")]
        public List<AlunoViewModel>? AlunosPresentes { get; set; }
    }
}
    