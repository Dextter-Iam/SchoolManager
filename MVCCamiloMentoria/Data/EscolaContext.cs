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
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Equipamento> Equipamentos { get; set; }
    public DbSet<PrestadorServico> PrestadoresServico { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Modelo> Modelos { get; set; }
    public DbSet<Diretor> Diretores { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<ProfessorTurma> ProfessoresTurmas { get; set; }
    public DbSet<Supervisor> Supervisores { get; set; }
    public DbSet<Coordenador> Coordenadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscolaContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

public DbSet<MVCCamiloMentoria.ViewModels.AlunoViewModel> AlunoViewModel { get; set; } = default!;
}
