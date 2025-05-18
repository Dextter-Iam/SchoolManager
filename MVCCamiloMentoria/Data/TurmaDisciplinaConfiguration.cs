using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

public class TurmaDisciplinaConfiguration : IEntityTypeConfiguration<TurmaDisciplina>
{
    public void Configure(EntityTypeBuilder<TurmaDisciplina> builder)
    {
        builder.ToTable("TurmaDisciplina");

        builder.HasKey(td => new { td.TurmaId, td.DisciplinaId });

        builder.HasOne(td => td.Turma)
               .WithMany(t => t.TurmaDisciplinas)
               .HasForeignKey(td => td.TurmaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(td => td.Excluido)
               .HasDefaultValue(false);

        builder.HasOne(td => td.Disciplina)
               .WithMany(d => d.TurmaDisciplinas)
               .HasForeignKey(td => td.DisciplinaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
