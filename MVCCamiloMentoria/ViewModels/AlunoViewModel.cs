using MVCCamiloMentoria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCCamiloMentoria.ViewModels
{
    public class AlunoViewModel
    {
        public int Id { get; set; }
        public string? NomeAluno { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? EmailEscolar { get; set; }
        public DateTime AnoInscricao { get; set; }
        public bool BolsaEscolar { get; set; }
        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }
        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public int EscolaId { get; set; }
        public Escola? Escola { get; set; }
        public List<Responsavel> Responsaveis { get; set; } = new List<Responsavel>();
        public List<Aula> Aulas { get; set; } = new List<Aula>();
    }
}
