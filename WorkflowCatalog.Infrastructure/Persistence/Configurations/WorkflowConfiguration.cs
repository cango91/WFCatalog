using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
    {
        public void Configure(EntityTypeBuilder<Workflow> builder)
        {
            builder.HasOne(x => x.Setup)
                .WithOne().
                IsRequired();
                
            builder.Property(k => k.Name)
                .IsRequired();
        }
    }
}
