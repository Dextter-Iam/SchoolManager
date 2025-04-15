using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Models
{
    public class Coordenador
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Telefone>? Telefones { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

    }
}
