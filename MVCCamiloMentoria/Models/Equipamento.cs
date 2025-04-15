namespace MVCCamiloMentoria.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool StatusOperacional { get; set; }
        public int MarcaId { get; set; }
        public Marca? Marca {get; set; }
        public int ModeloId { get; set; }
        public Modelo? Modelo { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola {get; set; }

    }
}
