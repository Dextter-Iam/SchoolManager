using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefone");
            builder.HasKey(t => t.Id);

            builder.Property(a => a.Numero)
                   .IsRequired()
                   .HasMaxLength(12);

            builder.Property(t => t.DDD)
                    .IsRequired()
                    .HasMaxLength(3);

            builder.Property(t => t.Excluido)
                   .HasDefaultValue(false);

            builder.HasOne(t => t.Escola)
                   .WithMany(e => e.Telefones)
                   .HasForeignKey(t => t.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
