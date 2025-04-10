using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

public class EscolaContext : DbContext
{
    public EscolaContext(DbContextOptions<EscolaContext> options) : base(options)
    {
    }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Disciplina> Disciplina { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Fornecedor> Fornecedore { get; set; }
    public DbSet<Equipamento> Equipamento { get; set; }
    public DbSet<PrestadorServico> PrestadoresServico { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Modelo> Modelos { get; set; }
    public DbSet<Diretor> Diretore { get; set; }
    public DbSet<Professor> Professore { get; set; }
    public DbSet<ProfessorTurma> ProfessoresTurma { get; set; }
    public DbSet<Supervisor> Supervisore { get; set; }
    public DbSet<Coordenador> Coordenador { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscolaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
