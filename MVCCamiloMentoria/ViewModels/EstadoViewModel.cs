using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class EstadoViewModel
    {
        public int id {  get; set; }
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public List<EnderecoViewModel>? Enderecos { get; set; }

    }
}
