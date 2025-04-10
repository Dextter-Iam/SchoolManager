using System.ComponentModel.DataAnnotations;

namespace MVCCamiloMentoria.Models
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        public int DDD { get; set; }

        public string? Numero { get; set; }
    }
}
