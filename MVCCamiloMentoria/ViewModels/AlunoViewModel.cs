using MVCCamiloMentoria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCCamiloMentoria.ViewModels
{
    [NotMapped]
    public class AlunoViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome Aluno")]
        public string? Nome { get; set; }

        [DisplayName("Nascimento")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Email Aluno")]
        public string? EmailEscolar { get; set; }

        [DisplayName("Ano de inscrição")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AnoInscricao { get; set; }

        [DisplayName("Bolsa Escolar")]
        public bool BolsaEscolar { get; set; }

        [DisplayName("Turma")]
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }

        [Phone]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "Telefone inválido. O formato correto é (XX) XXXXX-XXXX.")]
        public int TelefoneId { get; set; }
        public Telefone? Telefone { get; set; }

        [DisplayName("Endereço")]
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        [DisplayName("Estado")]
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }


        [DisplayName("Rua")]
        public string? NomeRua { get; set; }

        [DisplayName("CEP")]
        public int CEP { get; set; }

        [DisplayName("Número")]
        public int NumeroRua { get; set; }

        [DisplayName("Complemento")]
        public string? Complemento { get; set; }

        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Responsavel>? Responsaveis { get; set; }
        public List<Aula>? Aulas { get; set; }
    }
}

