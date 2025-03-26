using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Models
{
    public class Aluno
    {
        [Key]
        public int AlunoId { get; set; }

        [Required]
        public string? NomeAluno { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string? EmailEscolar { get; set; }

        [Required]
        public int AnoLetivo { get; set; }

        [Required]
        public DateTime AnoInscricao { get; set; }

        [Required]
        public bool BolsaEscolar { get; set; }

        [ForeignKey("TurmaId")]
        [DisplayName("Turma")]
        public int TurmaId { get; set; }

    }
}
