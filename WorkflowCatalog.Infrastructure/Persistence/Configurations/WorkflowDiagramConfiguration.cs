﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence.Configurations
{
    public class WorkflowDiagramConfiguration : IEntityTypeConfiguration<WorkflowDiagram>
    {
        public void Configure(EntityTypeBuilder<WorkflowDiagram> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasOne(x => x.Workflow)
                .WithMany(x => x.Diagrams)
                .IsRequired();
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.MimeType)
                .IsRequired();
        }
    }
}