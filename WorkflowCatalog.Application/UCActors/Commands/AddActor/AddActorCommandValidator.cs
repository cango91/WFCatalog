using System;
using FluentValidation;

namespace WorkflowCatalog.Application.UCActors.Commands.AddActor
{
    public class AddActorCommandValidator : AbstractValidator<AddActorCommand>
    {
        public AddActorCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(60).WithMessage("Actor name can not exceed 60 characters");
        }
    }
}
