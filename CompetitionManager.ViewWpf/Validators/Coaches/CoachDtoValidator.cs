using CompetitionManager.OpenAPIsConnector;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Validators.Coaches
{
    public class CreateUpdateCoachDtoValidator : AbstractValidator<CreateUpdateCoachDto>
    {
        public CreateUpdateCoachDtoValidator()
        {
            RuleFor(user => user.Surname)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(2, 50);
            RuleFor(user => user.Patronymic)
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
            RuleFor(user => user.AcademicDegree)
                .NotNull()
                .Length(3, 50);
        }
    }
}
