using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class DiretorViewModel
    {   
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }


        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public List<TelefoneViewModel>? Telefones { get; set; } = new List<TelefoneViewModel>();

        [DisplayName("Escola")]
        public int EscolaId { get; set; }

        public EscolaViewModel? Escola { get; set; }
    }
}
