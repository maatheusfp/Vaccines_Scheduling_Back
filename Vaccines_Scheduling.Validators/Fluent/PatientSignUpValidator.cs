using FluentValidation;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validators.Fluent
{
    public class PatientSignUpValidator : AbstractValidator<PatientSignUpModel>
    {
        public PatientSignUpValidator() 
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MinimumLength(5).WithMessage(string.Format(InfraMessages.MinSize, "Name", 3));

            RuleFor(t => t.Login)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MinimumLength(3).WithMessage(string.Format(InfraMessages.MinSize, "Login", 3))
                .MaximumLength(50).WithMessage(string.Format(InfraMessages.MaxSize, "Login", 50));

            RuleFor(t => t.Birthday)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .Must(BeAValidDate).WithMessage(InfraMessages.InvalidBirthday);


            RuleFor(t => t.Password)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MinimumLength(5).WithMessage(string.Format(InfraMessages.MinSize, "Password", 5));
        }
        private bool BeAValidDate(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
