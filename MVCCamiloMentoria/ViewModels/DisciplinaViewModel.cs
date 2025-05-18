using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class DisciplinaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        [Display(Name = "Nome da Disciplina")]
        public string? Nome { get; set; }

        [Display(Name = "Escola")]
        public int EscolaId { get; set; }

        public EscolaViewModel? Escola { get; set; }

        public List<TurmaDisciplinaViewModel>? TurmaDisciplinas { get; set; }

        //FILTROS 
        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Escola")]
        public string? FiltroEscola { get; set; }

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();

        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        private string NormalizarFiltro(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }
    }
}
