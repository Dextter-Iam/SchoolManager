namespace MVCCamiloMentoria.Models
{
    public class Estado
    {
        public int id {  get; set; }

        public string? Nome { get; set; }

        public string? Sigla { get; set; }
        public List<Endereco>? Enderecos { get; set; }

    }
}
