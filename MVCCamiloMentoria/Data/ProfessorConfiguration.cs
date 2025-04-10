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

            builder.Property(p => p.Matricula)
                   .HasMaxLength(6)
                   .IsRequired();


            builder.HasMany(d => d.Disciplinas)
                   .WithMany(p => p.Professores)
                   .UsingEntity(pd => pd.ToTable("ProfessorDisciplina"));

            builder.HasMany(p => p.Aulas) 
                   .WithOne(a => a.Professor)  
                   .HasForeignKey(a => a.ProfessorId) 
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.Professor)
                   .HasForeignKey(e => e.ProfessorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Endereco)
                   .WithOne()
                   .HasForeignKey<Professor>(p => p.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
