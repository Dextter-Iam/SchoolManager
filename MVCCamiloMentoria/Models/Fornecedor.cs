namespace MVCCamiloMentoria.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string? NomeEmpresa { get; set; }
        public int? CNPJ { get; set; }
        public int? CPF { get; set; }
        public string? FinalidadeFornecedor { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
    }
}
