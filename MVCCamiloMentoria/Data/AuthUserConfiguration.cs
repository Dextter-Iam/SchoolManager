using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUserModel>
{
    public void Configure(EntityTypeBuilder<AuthUserModel> builder)
    {
        builder.ToTable("AuthUser");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(u => u.UserName)
       .IsUnique();

        builder.Property(u => u.Password)
               .IsRequired()
               .HasMaxLength(100);
    }
}
