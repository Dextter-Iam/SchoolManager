using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class PrestadorServicoViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Prestador")]
        [Required(ErrorMessage = "O nome do prestador é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

        [DisplayName("CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
        public string? CPF { get; set; }

        [DisplayName("CNPJ")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
        public string? CNPJ { get; set; }

        [DisplayName("Nome da Empresa")]
        [StringLength(200, ErrorMessage = "O nome da empresa deve ter no máximo 200 caracteres.")]
        public string? EmpresaNome { get; set; }

        [DisplayName("Finalidade do Serviço")]
        [Required(ErrorMessage = "A finalidade do serviço é obrigatória.")]
        [StringLength(300, ErrorMessage = "A finalidade deve ter no máximo 300 caracteres.")]
        public string? ServicoFinalidade { get; set; }

        [DisplayName("Telefones")]
        public List<Telefone>? Telefones { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
    }
}
