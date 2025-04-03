using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("MarcaEquipamento");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nome)
                   .IsRequired()
                   .HasMaxLength(190);

            builder.Property(m => m.Descricao)
                   .IsRequired(false)
                   .HasMaxLength(600);

            builder.HasMany(m => m.Modelos)
                   .WithOne(mo => mo.Marca)
                   .HasForeignKey(mo => mo.MarcaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
