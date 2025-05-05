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
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Telefones")]
        public List<TelefoneViewModel>? Telefones { get; set; }
     
        [DisplayName("Alunos Relacionados")]
        public List<AlunoResponsavelViewModel>? AlunoResponsavel { get; set; }

        [DisplayName("IDs dos Alunos")]
        public List<int>? AlunoIds { get; set; }
    }
}
