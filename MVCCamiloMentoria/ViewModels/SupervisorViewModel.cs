using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCCamiloMentoria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class SupervisorViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        [DisplayName("Nome do Supervisor")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma matrícula válida.")]
        [DisplayName("Matrícula")]
        public int? Matricula { get; set; }
        public byte[]? Foto { get; set; }

        [DisplayName("Foto de Perfil")]
        public IFormFile? FotoUpload { get; set; }
        public string? FotoUrl { get; set; } 
        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }
        
        public List<int>? EscolaIds { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos uma escola.")]
        [DisplayName("Escolas")]
        public int EscolaId { get; set; }
        public List<SupervisorEscolaViewModel>? SupervisorEscola { get; set; }
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
