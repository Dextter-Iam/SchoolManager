using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class DiretorViewModel
    {   
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Matricula { get; set; }


        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public List<TelefoneViewModel>? Telefones { get; set; } = new List<TelefoneViewModel>();

        [DisplayName("Escola")]
        public int EscolaId { get; set; }

        public EscolaViewModel? Escola { get; set; }


        //FILTROS 
        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Escola")]
        public string? FiltroEscola { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Matrícula")]
        public string? FiltroMatricula { get; set; }

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();

        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        private string NormalizarFiltro(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }



    }
}
