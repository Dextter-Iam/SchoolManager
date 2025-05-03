using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class CoordenadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma matrícula válida.")]
        public int? Matricula { get; set; }

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public EscolaViewModel? Escola { get; set; }

        public List<TelefoneViewModel>? Telefones { get; set; }
    }
}
