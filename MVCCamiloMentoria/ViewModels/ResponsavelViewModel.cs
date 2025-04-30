using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class ResponsavelViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Responsável")]
        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome do responsável deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

     
        [DisplayName("Endereço")]
        public int? EnderecoId { get; set; }
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Nome da Rua")]
        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da rua deve ter no máximo 200 caracteres.")]
        public string? NomeRua { get; set; }

        [DisplayName("Número da Rua")]
        [Required(ErrorMessage = "O número da rua é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número da rua deve ser maior que zero.")]
        public int NumeroRua { get; set; }

        [DisplayName("Complemento")]
        [StringLength(150, ErrorMessage = "O complemento deve ter no máximo 150 caracteres.")]
        public string? Complemento { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter 8 dígitos numéricos.")]
        public string? CEP { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; }

  
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

  
        [DisplayName("DDD")]
        [Required(ErrorMessage = "O DDD é obrigatório.")]
        [Range(11, 99, ErrorMessage = "O DDD deve ser válido (entre 11 e 99).")]
        public int DDD { get; set; }

        [DisplayName("Número de Telefone")]
        [Required(ErrorMessage = "O número do telefone é obrigatório.")]
        [Range(10000000, 999999999, ErrorMessage = "Número de telefone inválido.")]
        public int Numero { get; set; }

        [DisplayName("Telefones")]
        public List<Telefone>? Telefones { get; set; }

     
        [DisplayName("Alunos Relacionados")]
        public List<AlunoResponsavel>? AlunoResponsavel { get; set; }

        [DisplayName("IDs dos Alunos")]
        public List<int>? AlunoIds { get; set; }
    }
}
