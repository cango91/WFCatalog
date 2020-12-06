using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    class UseCaseActorConfiguration : IEntityTypeConfiguration<UseCaseActor>
    {
        public void Configure(EntityTypeBuilder<UseCaseActor> builder)
        {
            builder.HasKey(ua => new { ua.UseCaseId, ua.ActorId });
            builder.HasOne<Actor>(ua => ua.Actor)
                .WithMany(a => a.UseCaseActors)
                .HasForeignKey(ua => ua.ActorId);
            builder.HasOne<UseCase>(ua => ua.UseCase)
                .WithMany(u => u.UseCaseActors)
                .HasForeignKey(ua => ua.UseCaseId);
        }
    }
}
