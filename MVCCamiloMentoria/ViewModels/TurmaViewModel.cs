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

        //FILTROS

        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Turno")]
        public string? FiltroTurno { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Escola")]
        public string? FiltroEscola { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Ano Letivo")]
        public string? FiltroAnoLetivo { get; set; }

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();


        [NotMapped]
        public string TurnoNormalizado => NormalizarFiltro(FiltroTurno);

        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        [NotMapped]
        public string EscolaNormalizado => NormalizarFiltro(FiltroEscola);

        [NotMapped]
        public string AnoLetivoNormalizado => NormalizarFiltro(FiltroAnoLetivo);

        private string NormalizarFiltro(string? input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }

    }
}
