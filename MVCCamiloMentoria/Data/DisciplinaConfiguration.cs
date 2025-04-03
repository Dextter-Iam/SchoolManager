using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplina");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(d => d.Professores)
                   .WithMany(p => p.Disciplinas)
                   .UsingEntity(pd => pd.ToTable("ProfessorDisciplina"));

            builder.HasMany(t => t.Aulas)
                   .WithOne()
                   .HasForeignKey(t => t.TurmaId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

