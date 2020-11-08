using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    public class UseCaseActorConfiguration : IEntityTypeConfiguration<UseCaseActor>
    {
        public void Configure(EntityTypeBuilder<UseCaseActor> builder)
        {
            builder.HasMany(x => x.UseCases);
            builder.HasOne(x => x.Setup)
                .WithMany(x => x.Actors)
                .IsRequired();
           
                
        }
    }
}
