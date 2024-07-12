using FluentValidation;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validators.Fluent
{
    public class PatientLoginValidator : AbstractValidator<PatientSignUpModel>
    {
        public PatientLoginValidator() 
        {
            RuleFor(t => t.Login)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));

            RuleFor(t => t.Password)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));
        }
    }
}
