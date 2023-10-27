using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManagement.Users.Domain.Common.ValueObjects;
using TodoManagement.Users.Domain.Users;
using TodoManagement.Users.Domain.Users.ValueObjects;

namespace TodoManagement.Users.Infrastructure.Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .HasColumnName("UserId")
            .IsRequired();

        builder
            .Property(p => p.Login)
            .HasColumnName("Login")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(p => p.Password)
            .HasColumnName("Password")
            .HasConversion(
                password => password.PasswordValue,
                passwordValue => Password.Create(passwordValue).Value)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value)
            .IsRequired();

        builder
            .Property(p => p.IsActive)
            .HasColumnName("IsActive")
            .IsRequired();

        builder
            .Property(p => p.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder
            .Property(p => p.CreatedDateTime)
            .HasColumnName("CreatedDateTime")
            .IsRequired();
    }
}
