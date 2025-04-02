using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class ProfessorTurmaConfiguration : IEntityTypeConfiguration<ProfessorTurma>
    {
        public void Configure(EntityTypeBuilder<ProfessorTurma> builder)
        {
            builder.HasKey(pt=>pt.ProfessorTurmaId);

            builder.HasOne(pt => pt.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(pt => pt.ProfessorId);

            builder.HasOne(pt => pt.Turma)
                .WithMany(p => p.Professores)
                .HasForeignKey(pt => pt.TurmaId);

            builder.ToTable("ProfessorTurma");
        }
    }
}
