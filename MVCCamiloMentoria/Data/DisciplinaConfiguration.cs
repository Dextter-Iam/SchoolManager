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

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Nome)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(d => d.Aulas)
                   .WithOne(a => a.Disciplina)
                   .HasForeignKey(a => a.DisciplinaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Escola)
                   .WithMany(e => e.Disciplina)
                   .HasForeignKey(d => d.EscolaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.TurmaDisciplinas)
                   .WithOne(td => td.Disciplina)
                   .HasForeignKey(td => td.DisciplinaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
