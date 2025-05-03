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

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [DisplayName("Estado")]
        public EstadoViewModel? Estado { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos uma escola.")]
        [DisplayName("Escolas")]
        public List<SupervisorEscolaViewModel>? Escolas { get; set; }
        public List<TelefoneViewModel>? Telefones { get; set; }
    }
}
