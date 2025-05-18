using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.Models
{
    public class PrestadorServico
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public string? EmpresaNome { get; set; }
        public string? ServicoFinalidade {  get; set; }
        public List<Telefone>? Telefones { get; set; }
        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
    }
}
