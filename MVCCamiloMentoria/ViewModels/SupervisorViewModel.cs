using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class SupervisorViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public List<Escola>? Escolas { get; set; }
        public List<Telefone>? Telefones { get; set; }

    }
}
