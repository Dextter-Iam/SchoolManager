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
        public Endereco? Endereco { get; set; }

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
        public string? CEP { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; }

        [DisplayName("Escola")]
        [Required(ErrorMessage = "A escola é obrigatória.")]
        public int? EscolaId { get; set; }

        [ForeignKey("EscolaId")]
        public Escola? Escola { get; set; }

        public List<Telefone>? Telefones { get; set; }
    }
}
