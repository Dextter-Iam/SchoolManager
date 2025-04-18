using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

public class EscolaContext : DbContext
{
    public EscolaContext(DbContextOptions<EscolaContext> options) : base(options)
    {
    }
    public DbSet<Turma> Turma { get; set; }
    public DbSet<AlunoTelefone> AlunoTelefone { get; set; }
    public DbSet<Aluno> Aluno { get; set; }
    public DbSet<AlunoResponsavel> AlunoResponsavel { get; set; }
    public DbSet<Aula> Aula { get; set; }
    public DbSet<Disciplina> Disciplina { get; set; }
    public DbSet<Estado> Estado { get; set; }
    public DbSet<Escola> Escola { get; set; }   
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Fornecedor> Fornecedor { get; set; }
    public DbSet<Equipamento> Equipamento { get; set; }
    public DbSet<PrestadorServico> PrestadorServico { get; set; }
    public DbSet<Marca> Marca { get; set; }
    public DbSet<Telefone> Telefone { get; set; }
    public DbSet<Modelo> Modelo { get; set; }
    public DbSet<Diretor> Diretor { get; set; }
    public DbSet<Professor> Professor { get; set; }
    public DbSet<ProfessorTurma> ProfessorTurma { get; set; }
    public DbSet<Supervisor> Supervisor { get; set; }
    public DbSet<Coordenador> Coordenador { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscolaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
