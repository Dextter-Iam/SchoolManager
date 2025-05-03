using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class EnderecoViewModel
    {
        public int Id { get; set; }
        public string? NomeRua { get; set; }
        public int? CEP { get; set; }
        public int NumeroRua {get; set;}
        public string? Complemento { get; set; }
        public int EstadoId { get; set; }
        public List<EstadoViewModel>? ListaDeEstados { get; set; } 
    }
}
