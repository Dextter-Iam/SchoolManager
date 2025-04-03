namespace MVCCamiloMentoria.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public string? NomeRua { get; set; }
        public int? CEP { get; set; }
        public int NumeroRua {get; set;}
        public string? Complemento { get; set; }
        public string? Estado {  get; set; }
        public string? Cidade { get; set; }
    }
}
