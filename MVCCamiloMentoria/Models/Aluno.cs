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
        public int Id { get; set; }

        public string? NomeAluno { get; set; }

        public DateTime DataNascimento { get; set; }

        public string? EmailEscolar { get; set; }

        public DateTime AnoInscricao { get; set; }

        public bool BolsaEscolar { get; set; }

        public int TurmaId { get; set; }

        public Turma Turma { get; set; }

        public int EnderecoId { get; set; }

        public Endereco? Endereco { get; set; }

        public int Telefone {  get; set; }  

        public bool ConfirmacaoPresenca {  get; set; }
    }
}
