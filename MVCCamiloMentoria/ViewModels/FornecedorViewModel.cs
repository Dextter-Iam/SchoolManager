using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class FornecedorViewModel
    {   
        public int Id { get; set; }

        [DisplayName("Nome Empresa")]
        public string? Nome { get; set; }

        [DisplayName("CNPJ Empresa")]
        public string? CNPJ { get; set; }

        [DisplayName("CPF")]
        public string? CPF { get; set; }

        [DisplayName("Categoria")]
        public string? FinalidadeFornecedor { get; set; }

        [DisplayName("Escola")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
    }
}
