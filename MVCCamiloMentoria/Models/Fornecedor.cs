namespace MVCCamiloMentoria.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? FinalidadeFornecedor { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
    }
}
