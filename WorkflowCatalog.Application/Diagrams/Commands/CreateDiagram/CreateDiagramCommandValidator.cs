using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileSignatures;
using FileSignatures.Formats;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Diagrams.Queries.GetDiagramById;
using WorkflowCatalog.Domain.ValueObjects;

namespace WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram
{
    class CreateDiagramCommandValidator : AbstractValidator<CreateDiagramCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileFormatInspector _inspector;
        private readonly IMapper _mapper;
        public CreateDiagramCommandValidator(IApplicationDbContext context, IFileFormatInspector inspector, IMapper mapper)
        {
            _context = context;
            _inspector = inspector;
            _mapper = mapper;

            RuleFor(x => x.File)
                .Must(BeAcceptableFileType).WithMessage("Only image, pdf and Microsoft Visio filetypes are accepted.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Diagram name can not be blank")
                .MustAsync(BeUniqueFilename).WithMessage("A file with the same name already exists for the workflow");
        }

        public bool BeAcceptableFileType(byte[] byteArray)
        {
            Stream stream = new MemoryStream(byteArray);
            var format = _inspector.DetermineFileFormat(stream);
            return (format is Pdf) || (format is Image) || (format is Visio) || (format is VisioLegacy);
        }

        public async Task<bool> BeUniqueFilename(CreateDiagramCommand command, string name, CancellationToken cancellationToken)
        {
            var diags = await _context.Diagrams
                .Where(x => x.Workflow.Id == command.WorkflowId)
                .ToListAsync(cancellationToken);
                return !diags.Any(x => x.Name.ToString().ToLower() == name.ToLower());
        }
    }
}
