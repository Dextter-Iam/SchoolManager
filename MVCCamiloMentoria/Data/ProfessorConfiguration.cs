using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professor");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Excluido)
                   .HasDefaultValue(false);

            builder.Property(p => p.Matricula)
                   .IsRequired();

            builder.HasOne(p => p.Escola)
                   .WithMany(p => p.Professores)
                   .HasForeignKey(p => p.EscolaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Endereco)
                   .WithOne()
                   .HasForeignKey<Professor>(p => p.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Telefones)
                   .WithOne(t => t.Professor)
                   .HasForeignKey(t => t.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Aulas)
                   .WithOne(a => a.Professor)
                   .HasForeignKey(a => a.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Turmas)
                   .WithOne(pt => pt.Professor)
                   .HasForeignKey(pt => pt.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Disciplinas)
                   .WithOne(pd => pd.Professor)
                   .HasForeignKey(pd => pd.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
