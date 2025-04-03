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
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }  
    }
}
