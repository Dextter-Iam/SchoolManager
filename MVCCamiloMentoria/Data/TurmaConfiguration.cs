using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("Turma");

        builder.HasKey(t => t.TurmaId);

        builder.Property(t => t.NomeTurma)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(t => t.AnoLetivo)
               .IsRequired();

        builder.Property(t => t.Turno)
               .IsRequired()
               .HasMaxLength(10);

        builder.HasOne(t => t.Escola)
               .WithMany(e => e.Turmas)
               .HasForeignKey(t => t.EscolaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Alunos)
               .WithOne(a => a.Turma)
               .HasForeignKey(a => a.TurmaId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(t => t.TurmaDisciplinas)
               .WithOne(td => td.Turma)
               .HasForeignKey(td => td.TurmaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Aulas)
               .WithOne(a => a.Turma)
               .HasForeignKey(a => a.TurmaId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
