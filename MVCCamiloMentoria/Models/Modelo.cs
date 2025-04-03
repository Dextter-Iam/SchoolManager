namespace MVCCamiloMentoria.Models
{
    public class Modelo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int MarcaId { get; set; }
        public Marca? Marca { get; set; }
        public List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();

    }
}
