using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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
        public Turma? Turma { get; set; }

        [DisplayName("DDD")]
        [Required(ErrorMessage = "Informe o DDD")]
        [Range(11, 99, ErrorMessage = "DDD inválido")]
        public int DDD { get; set; }

        [DisplayName("Número de Telefone")]
        [Required(ErrorMessage = "Informe o número")]
        [Range(10000000, 999999999, ErrorMessage = "Número inválido")]
        public int Numero { get; set; }

        [DisplayName("Endereço")]
        public int? EnderecoId { get; set; }
        public EnderecoViewModel? Endereco { get; set; }

        [DisplayName("Nome da Rua")]
        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da rua deve ter no máximo 200 caracteres.")]
        public string? NomeRua { get; set; }

        [DisplayName("Número da Rua")]
        [Required(ErrorMessage = "O número da rua é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O número da rua deve ser maior que zero.")]
        public int NumeroRua { get; set; }

        [DisplayName("Complemento")]
        [StringLength(150, ErrorMessage = "O complemento deve ter no máximo 150 caracteres.")]
        public string? Complemento { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [Range(1000000, 99999999, ErrorMessage = "CEP inválido.")]
        public int CEP { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public List<EstadoViewModel>? Estados { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }

        [DisplayName("Responsável")]
        public int? ResponsavelId { get; set; }

        public SelectList? ResponsaveisDisponiveis { get; set; }

        [DisplayName("Responsável")]
        public List<AlunoResponsavel>? AlunoResponsavel { get; set; }
   
        [DisplayName("Responsável Atual")]
        public string NomeResponsavelAtual =>
            AlunoResponsavel?.FirstOrDefault()?.Responsavel?.Nome ?? "Nenhum responsável";
        public ResponsavelViewModel? Responsavel { get; set; }
        public byte[]? Foto { get; set; }
        public IFormFile? FotoUpload { get; set; }
        public List<AlunoTelefone>? AlunoTelefone { get; set; }
        public List<Aula>? Aulas { get; set; }


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