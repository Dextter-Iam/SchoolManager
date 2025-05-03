using MVCCamiloMentoria.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class AlunoViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome do aluno deve ter no máximo 200 caracteres.")]
        public string? Nome { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O e-mail do aluno é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string? EmailEscolar { get; set; }

        [DisplayName("Ano de Inscrição")]
        [Required(ErrorMessage = "O ano de inscrição é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime AnoInscricao { get; set; }

        [DisplayName("Bolsa Escolar")]
        public bool BolsaEscolar { get; set; }

        [DisplayName("Turma")]
        [Required(ErrorMessage = "A turma é obrigatória.")]
        public int TurmaId { get; set; }
        public List<TurmaViewModel>? Turmas { get; set; }

        [DisplayName("Endereço")]
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }
        public List<EscolaViewModel>? Escolas { get; set; }

        [DisplayName("Responsável")]
        public int? ResponsavelId { get; set; }
        public SelectList? ResponsaveisDisponiveis { get; set; }

        [DisplayName("Responsáveis")]
        public List<AlunoResponsavelViewModel>? Responsaveis { get; set; }

        [DisplayName("Responsável Atual")]
        public ResponsavelViewModel? Responsavel { get; set; }

        public byte[]? Foto { get; set; }
        public IFormFile? FotoUpload { get; set; }

        public List<AlunoTelefoneViewModel>? Telefones { get; set; }
        public List<AulaViewModel>? Aulas { get; set; }

        [DisplayName("Responsável 1")]
        public string? NomeResponsavel1 { get; set; }

        [DisplayName("Parentesco 1")]
        public string? Parentesco1 { get; set; }

        [DisplayName("Responsável 2")]
        public string? NomeResponsavel2 { get; set; }

        [DisplayName("Parentesco 2")]
        public string? Parentesco2 { get; set; }

        public SelectList? ParentescoOptions { get; set; }

        [NotMapped]
        public int ResponsavelIdValue => ResponsavelId ?? 0;
    }
}
