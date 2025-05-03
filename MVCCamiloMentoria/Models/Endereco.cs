using System.ComponentModel.DataAnnotations;

namespace MVCCamiloMentoria.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string? NomeRua { get; set; }
        public int? CEP { get; set; }
        public int NumeroRua {get; set;}
        public string? Complemento { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado {  get; set; }
        public List<Aluno>? Alunos { get; set; }
    }
}
