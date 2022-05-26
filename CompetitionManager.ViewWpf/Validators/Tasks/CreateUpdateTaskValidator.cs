using CompetitionManager.OpenAPIsConnector;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Validators.Tasks
{
    public class CreateUpdateTaskValidator : AbstractValidator<CreateUpdateTaskDto>
    {
        public CreateUpdateTaskValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 100);
            RuleFor(user => user.Text)
                .NotEmpty()
                .NotNull();
            RuleFor(user => user.Solution)
                .NotEmpty()
                .NotNull();
            RuleFor(user => user.OutputExample)
                .NotEmpty()
                .NotNull();
            RuleFor(user => user.InputExample)
                .NotEmpty()
                .NotNull();
        }
    }
}
