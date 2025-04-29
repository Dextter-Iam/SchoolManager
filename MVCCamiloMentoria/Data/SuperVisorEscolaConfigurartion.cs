using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Data
{
    public class SupervisorEscolaConfiguration : IEntityTypeConfiguration<SupervisorEscola>
    {
        public void Configure(EntityTypeBuilder<SupervisorEscola> builder)
        {
            builder.ToTable("SupervisorEscola");

            builder.HasKey(se => se.SupervisorEscolaId); // Chave primária é o ID

            builder.HasOne(se => se.Supervisor)
                   .WithMany(s => s.SupervisorEscolas)
                   .HasForeignKey(se => se.SupervisorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(se => se.Escola)
                   .WithMany(e => e.SupervisorEscolas)
                   .HasForeignKey(se => se.EscolaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
