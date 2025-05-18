using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]  
    public class EquipamentoViewModel
    {
      
        public int Id { get; set; }

 
        [Display(Name = "Nome do Equipamento")]
        [Required(ErrorMessage = "O nome do equipamento é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do equipamento não pode exceder 100 caracteres.")]
        public string? Nome { get; set; }

        [Display(Name = "Status Operacional")]
        public bool StatusOperacional { get; set; }

       
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Selecione uma marca.")]
        public int MarcaId { get; set; }

       
        public Marca? Marca { get; set; }


        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Selecione um modelo.")]
        public int ModeloId { get; set; }

        public Modelo? Modelo { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "Selecione uma escola.")]
        public int EscolaId { get; set; }

        public Escola? Escola { get; set; }


        //FILTROS 
        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Escola")]
        public string? FiltroEscola { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Marca")]
        public string? FiltroMarca { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Modelo")]
        public string? FiltroModelo { get; set; }

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();


        [NotMapped]
        public string ModeloNormalizado => NormalizarFiltro(FiltroModelo);

        [NotMapped]
        public string MarcaNormalizada => NormalizarFiltro(FiltroMarca);

        [NotMapped]
        public string EscolaNormalizada => NormalizarFiltro(FiltroEscola);


        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        private string NormalizarFiltro(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }
    }
}
