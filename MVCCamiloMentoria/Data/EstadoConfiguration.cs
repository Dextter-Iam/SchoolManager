using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");
            builder.HasKey(e => e.id);

            builder.Property(e => e.Nome)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.Sigla)
                   .HasMaxLength(3);

            builder.HasMany(e => e.Enderecos)
                   .WithOne(e => e.Estado)
                   .HasForeignKey(e => e.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
