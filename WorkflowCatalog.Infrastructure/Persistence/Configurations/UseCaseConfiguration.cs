using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    public class UseCaseConfiguration : IEntityTypeConfiguration<UseCase>
    {
        public void Configure(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.Workflow)
                .WithMany(s => s.UseCases)
                .IsRequired();
            
            //builder.Property(t => t.Workflow)
            //    .IsRequired();
            
            builder.HasMany(x => x.Actors);
                

        }
    }
}
