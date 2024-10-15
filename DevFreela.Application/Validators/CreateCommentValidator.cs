using DevFreela.Application.Commands.InsertComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentValidator : AbstractValidator<InsertCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(p => p.Content)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Texto de Comentário é de 255 caracteres.");
        }
    }
}
