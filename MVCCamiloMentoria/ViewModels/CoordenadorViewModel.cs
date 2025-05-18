using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class CoordenadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma matrícula válida.")]
        public int? Matricula { get; set; }

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]

        public int EscolaId { get; set; }
        public EscolaViewModel? Escola { get; set; }

        public List<TelefoneViewModel>? Telefones { get; set; }


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
