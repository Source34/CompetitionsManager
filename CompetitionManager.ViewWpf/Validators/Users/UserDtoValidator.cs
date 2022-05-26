using CompetitionManager.OpenAPIsConnector;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Validators.Users
{
    public class CreateUpdateUserDtoValidator : AbstractValidator<CreateUpdateUserDto>
    {
        public CreateUpdateUserDtoValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
            RuleFor(user => user.Surname)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
            RuleFor(user => user.CodeforcesLogin)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);
            RuleFor(user => user.University)
                .NotEmpty()
                .NotNull()
                .Length(3, 50);
        }
    }
}
