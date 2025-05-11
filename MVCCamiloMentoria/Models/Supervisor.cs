namespace MVCCamiloMentoria.Models
{
    public class Supervisor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }
        public bool Excluido { get; set; } = false;
        public int EnderecoId { get; set; }
        public byte[]? Foto { get; set; }
        public string? FotoUrl { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public List<SupervisorEscola>? SupervisorEscolas { get; set; }

    }
}
