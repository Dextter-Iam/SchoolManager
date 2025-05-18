using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class FornecedorViewModel
    {   
        public int Id { get; set; }

        [DisplayName("Nome Empresa")]
        public string? Nome { get; set; }

        [DisplayName("CNPJ Empresa")]
        public string? CNPJ { get; set; }

        [DisplayName("CPF")]
        public string? CPF { get; set; }

        [DisplayName("Categoria")]
        public string? FinalidadeFornecedor { get; set; }

        [DisplayName("Escola")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }


        //FILTROS

        [NotMapped]
        [DisplayName("Buscar por qualquer campo")]
        public string? FiltroGeral { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Nome")]
        public string? FiltroNome { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por CNPJ")]
        public string? FiltroCNPJ { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por CPF")]
        public string? FiltroCPF { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Categoria")]
        public string? FiltroFinalidadeFornecedor { get; set; }

        [NotMapped]
        [DisplayName("Filtrar por Escola")]
        public string? FiltroEscola { get; set; }

        [NotMapped]
        public string NomeNormalizado => NormalizarFiltro(FiltroNome);

        [NotMapped]
        public string CNPJNormalizado => NormalizarFiltro(FiltroCNPJ);

        [NotMapped]
        public string CPFNormalizado => NormalizarFiltro(FiltroCPF);

        [NotMapped]
        public string FinalidadeFornecedorNormalizado => NormalizarFiltro(FiltroFinalidadeFornecedor);

        [NotMapped]
        public string EscolaNormalizada => NormalizarFiltro(FiltroEscola);

        [NotMapped]
        public string FiltroGeralNormalizado => string.IsNullOrWhiteSpace(FiltroGeral) ? string.Empty : FiltroGeral.Trim().ToLower();
        private string NormalizarFiltro(string? input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLower();
        }
    }
}
