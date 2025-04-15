using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class PrestadorServico
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? CPF { get; set; }
        public int? CNPJ { get; set; }
        public string? EmpresaNome { get; set; }
        public string? ServicoFinalidade {  get; set; }
        public List<Telefone>? Telefones { get; set; }

        [ForeignKey("EscolaId")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }  
    }
}
