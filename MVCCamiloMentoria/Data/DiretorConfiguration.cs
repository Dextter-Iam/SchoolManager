using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class DiretorConfiguration : IEntityTypeConfiguration<Diretor>
    {
        public void Configure(EntityTypeBuilder<Diretor> builder)
        {
            builder.ToTable("Diretor");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Matricula)
                   .HasMaxLength(6)
                   .IsRequired();

            builder.Property(p => p.Excluido)
                   .HasDefaultValue(false);

            builder.HasMany(c => c.Telefones)
                   .WithOne(e => e.Diretor)
                   .HasForeignKey(t => t.DiretorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Endereco)
                   .WithOne()
                   .HasForeignKey<Diretor>(c => c.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
