using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da rua não pode ter mais que 100 caracteres.")]
        [DisplayName("Nome da Rua")]
        public string? NomeRua { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 dígitos numéricos.")]
        public string? CEPMascara
        {
            get => CEP.HasValue ? CEP.Value.ToString("D8") : null;
            set
            {
                if (int.TryParse(value, out var result))
                    CEP = result;
                else
                    CEP = null;
            }
        }

        [ScaffoldColumn(false)] 
        public int? CEP { get; set; }

        [Required(ErrorMessage = "O número da rua é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número da rua deve ser um valor positivo.")]
        [DisplayName("Número da Rua")]
        public int NumeroRua { get; set; }

        [StringLength(200, ErrorMessage = "O complemento não pode ter mais que 200 caracteres.")]
        [DisplayName("Complemento")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [DisplayName("Estado")]
        public int EstadoId { get; set; }

        public List<EstadoViewModel>? Estado { get; set; }
    }
}
