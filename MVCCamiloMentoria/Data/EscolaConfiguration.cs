using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class EscolaConfiguration : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.HasKey(e => e.EscolaId);

            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);

            builder.Property(e => e.EnderecoId).IsRequired().HasMaxLength(250);

            builder.HasMany(e => e.Turmas)
                   .WithOne(t => t.Escola)
                   .HasForeignKey(e => e.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Escolas");
        }
    }
}
