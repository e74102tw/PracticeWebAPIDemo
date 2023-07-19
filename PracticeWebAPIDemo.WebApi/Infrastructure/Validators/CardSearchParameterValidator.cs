using FluentValidation;
using PracticeWebAPIDemo.Models.InputParameters;

namespace PracticeWebAPIDemo.Infrastructure.Validators
{
    public class CardSearchParameterValidator : AbstractValidator<CardSearchParameter>
    {
        public CardSearchParameterValidator()
        {
            this.When(w => w.MinAttack.HasValue, () =>
            {
                this.RuleFor(r => r.MinAttack)
                    .Must(m => m.Value >= 0)
                    .WithMessage("MinAttack 不可負數!");
            });

            this.When(w => w.MaxAttack.HasValue, () =>
            {
                this.RuleFor(r => r.MaxAttack)
                    .Must(m => m.Value >= 0)
                    .WithMessage("MaxAttack 不可負數!");
            });

            this.When(w => w.MinHealth.HasValue, () =>
                {
                    this.RuleFor(r => r.MinHealth)
                        .Must(m => m.Value >= 0)
                        .WithMessage("MinHealth 不可負數!");
                });

            this.When(w => w.MaxHealth.HasValue, () =>
            {
                this.RuleFor(r => r.MaxHealth)
                    .Must(m => m.Value >= 0)
                    .WithMessage("MaxHealth 不可負數!");
            });

            this.When(w => w.MinCost.HasValue, () =>
            {
                this.RuleFor(r => r.MinCost)
                    .Must(m => m.Value >= 0)
                    .WithMessage("MinCost 不可負數!");
            });

            this.When(w => w.MaxCost.HasValue, () =>
            {
                this.RuleFor(r => r.MaxCost)
                    .Must(m => m.Value >= 0)
                    .WithMessage("MaxCost 不可負數!");
            });
        }
    }
}
