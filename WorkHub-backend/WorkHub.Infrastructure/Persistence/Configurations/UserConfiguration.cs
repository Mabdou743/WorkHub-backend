using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkHub.Infrastructure.Identity.Entity;

namespace WorkHub.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.IdentityUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(u => u.IdentityUserId).IsUnique();
            builder.HasIndex(p => p.CompanyId);

            // Relationships
            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<User>(u => u.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.TeamMembers)
                .WithOne(tm => tm.User)
                .HasForeignKey(tm => tm.UserId);

            builder.HasMany(u => u.TaskAssignments)
                .WithOne(ta => ta.User)
                .HasForeignKey(ta => ta.UserId);
        }
    }
}
