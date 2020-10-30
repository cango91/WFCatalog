using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram
{
    public class CreateDiagramCommandValidator : AbstractValidator<CreateDiagramCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateDiagramCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.File)
                .NotEmpty().WithMessage("A file must be provided for workflow diagram.");
            RuleFor(x => x.WorkflowId)
                .NotEmpty().WithMessage("A diagram must belong to an existing workflow.")
                .MustAsync(HaveValidWorkflow);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("A name for the diagram must be provided.")
                .MustAsync(HaveUniqueNameForWorkflowOrDifferentMimeType).WithMessage(
                "Name for the diagram must be unique for the workflow (or have a different filetype than the existing name)");
            RuleFor(x => x.MimeType)
                .NotEmpty().WithMessage("Mime Type must be specified.");
               
        }

        public async Task<bool> HaveUniqueNameForWorkflowOrDifferentMimeType(CreateDiagramCommand model, string name, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows.Include(s => s.Diagrams).FirstOrDefaultAsync(s => s.Id == model.WorkflowId);
            var diags = entity.Diagrams.Where(x => x.Name.ToLower() == name.ToLower());
            return diags.Count() == 0 ? true : !diags.Any(x => x.MimeType == model.MimeType);
        }

        public async Task<bool> HaveValidWorkflow(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows.FindAsync(id);
            return entity != null;
        }
    }
}
