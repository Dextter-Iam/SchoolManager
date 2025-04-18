using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class ModeloConfiguration : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.ToTable("ModeloEquipamento");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nome)
                   .IsRequired()
                   .HasMaxLength(190);

            builder.Property(m => m.Descricao)
                   .IsRequired()
                   .HasMaxLength(600);

            builder.HasOne(m => m.Marca)
                   .WithMany(m => m.Modelos)
                   .HasForeignKey(m => m.MarcaId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
