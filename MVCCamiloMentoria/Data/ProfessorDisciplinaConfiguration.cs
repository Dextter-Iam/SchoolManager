using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

public class ProfessorDisciplinaConfiguration : IEntityTypeConfiguration<ProfessorDisciplina>
{
    public void Configure(EntityTypeBuilder<ProfessorDisciplina> builder)
    {
        builder.ToTable("ProfessorDisciplina");

        builder.HasKey(pd => pd.ProfessorDisciplinaId);

        builder.HasOne(pd => pd.Professor)
            .WithMany(p => p.Disciplinas)
            .HasForeignKey(pd => pd.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pd => pd.Disciplina)
            .WithMany(d => d.Professores)
            .HasForeignKey(pd => pd.DisciplinaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
