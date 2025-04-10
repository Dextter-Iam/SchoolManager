using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class SuperVisorConfigurartion : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {
            builder.ToTable("Supervisor");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.Matricula)
                   .HasMaxLength(6)
                   .IsRequired();

            builder.HasOne(s => s.Endereco)
                   .WithOne()
                   .HasForeignKey<Supervisor>(s => s.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Telefones)
                   .WithOne(e => e.Supervisor)
                   .HasForeignKey(e => e.SupervisorId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(s => s.Escolas)
                   .WithMany();

        }
    }
}
