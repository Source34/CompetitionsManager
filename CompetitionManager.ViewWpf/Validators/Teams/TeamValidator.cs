using CompetitionManager.OpenAPIsConnector;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Validators.Teams
{
    public class TeamValidator : AbstractValidator<TeamDto>
    {
        public TeamValidator()
        {
            RuleFor(team => team.Name)
                .NotEmpty()
                .NotNull()
                .Length(2, 30);
            RuleFor(team => team.Coach)
                .NotNull();
        }
    }
}
