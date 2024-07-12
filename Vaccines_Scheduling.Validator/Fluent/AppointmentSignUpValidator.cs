using FluentValidation;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validator.Fluent
{
    public class AppointmentSignUpValidator : AbstractValidator<AppointmentSignUpModel>
    {
        public AppointmentSignUpValidator() 
        {
            RuleFor(t => t.Date)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage(InfraMessages.DateBefore);

            RuleFor(t => t.Time.Hour)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .InclusiveBetween(0, 23).WithMessage(InfraMessages.InvalidHour);

            RuleFor(t => t.Birthday)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));

            RuleFor(t => t.PatientName)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));
        }
    }
}
