using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.Models
{
    public class Diretor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }
        public int EnderecoId { get; set; }
        public EnderecoViewModel? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public int EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }
    }
}
