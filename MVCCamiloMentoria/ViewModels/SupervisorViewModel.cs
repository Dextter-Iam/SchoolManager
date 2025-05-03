using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class SupervisorViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        [DisplayName("Nome do Supervisor")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma matrícula válida.")]
        [DisplayName("Matrícula")]
        public int? Matricula { get; set; }

        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(11, 99, ErrorMessage = "Informe um DDD válido.")]
        [DisplayName("DDD")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [Range(10000000, 999999999, ErrorMessage = "Número de telefone inválido.")]
        [DisplayName("Número de Telefone")]
        public int Numero { get; set; }

        [DisplayName("Endereço")]
        public int? EnderecoId { get; set; }
        public EnderecoViewModel? Endereco { get; set; }

        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da rua deve ter no máximo 200 caracteres.")]
        [DisplayName("Nome da Rua")]
        public string? NomeRua { get; set; }

        [Required(ErrorMessage = "O número da rua é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um número de rua válido.")]
        [DisplayName("Número da Rua")]
        public int NumeroRua { get; set; }

        [StringLength(150, ErrorMessage = "O complemento deve ter no máximo 150 caracteres.")]
        [DisplayName("Complemento")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 números.")]
        [DisplayName("CEP")]
        public string? CEP { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [DisplayName("Estado")]
        public int? EstadoId { get; set; }
        public EstadoViewModel? Estado { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos uma escola.")]
        [DisplayName("Escolas")]
        public List<int>? EscolaIds { get; set; }

        public List<SupervisorEscolaViewModel>? Escolas { get; set; }

        public List<TelefoneViewModel>? Telefones { get; set; }
    }
}
