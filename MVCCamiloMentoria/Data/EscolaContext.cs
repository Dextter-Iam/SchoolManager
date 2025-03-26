using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Data

    //CONFIGURAÇÃO RESPONSÁVEL PELO BANCO DE DADOS
{
    public class EscolaContext : DbContext
    {
        public EscolaContext(DbContextOptions<EscolaContext>options) : base(options)
        { 

        }
        //MAPEANDO OS DADOS QUE VOU UTILIZAR NO BANCO
        public DbSet<Turma> turmas { get; set; }   
        public DbSet<Aluno> alunos { get; set; }
    }
}
