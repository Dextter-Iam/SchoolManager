using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Aluno");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(a => a.EmailEscolar)
               .HasMaxLength(200);


        builder.Property(a => a.BolsaEscolar)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne(a => a.Turma)
               .WithMany(t => t.Alunos)
               .HasForeignKey(a => a.TurmaId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(a => a.Endereco)
               .WithOne()
               .HasForeignKey<Aluno>(a => a.EnderecoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Responsaveis)
               .WithMany(r => r.Alunos)
               .UsingEntity(j => j.ToTable("AlunoResponsavel"));
    }
}