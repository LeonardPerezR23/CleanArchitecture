using FluentValidation;

namespace CleanArquitecture.Application.Features.Streamers.Commands.UppdateStreamer
{
    public class UpdatStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdatStreamerCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotNull().WithMessage("{Nombre} no permite valores nulos");

            RuleFor(p => p.Url)
                .NotNull().WithMessage("{Url} no permite valores nulos");
        }
    }
}
