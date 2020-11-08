using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    public class SetupConfiguration : IEntityTypeConfiguration<Setup>
    {
        public void Configure(EntityTypeBuilder<Setup> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(6);
            builder.Property(l => l.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Workflows)
                .WithOne(k=>k.Setup);
            builder.HasMany(x => x.Actors)
                .WithOne(x => x.Setup);
        }
    }
}
