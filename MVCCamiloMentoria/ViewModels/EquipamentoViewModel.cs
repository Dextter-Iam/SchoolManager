using MVCCamiloMentoria.Models;
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
    }
}
