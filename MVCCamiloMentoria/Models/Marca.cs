namespace MVCCamiloMentoria.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public List<Modelo>? Modelos { get; set; }

    }
}
