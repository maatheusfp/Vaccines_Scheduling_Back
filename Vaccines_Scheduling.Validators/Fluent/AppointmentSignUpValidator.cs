using FluentValidation;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validators.Fluent
{
    public class AppointmentSignUpValidator : AbstractValidator<AppointmentSignUpModel>
    {
        public AppointmentSignUpValidator() 
        {
            RuleFor(t => t.Date)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage(InfraMessages.InvalidDateAppointment);

            RuleFor(t => t.Time.Hour)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .InclusiveBetween(0, 23).WithMessage(InfraMessages.InvalidHour);

            RuleFor(t => t.Birthday)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .Must(BeAValidDate).WithMessage(InfraMessages.InvalidBirthday);

            RuleFor(t => t.Birthday.Month)
                .InclusiveBetween(1, 12).WithMessage(InfraMessages.InvalidMonth);

            RuleFor(t => t.Birthday.Day)
                .InclusiveBetween(1, 31).WithMessage(InfraMessages.InvalidDay);

            RuleFor(t => t.PatientName)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MinimumLength(3).WithMessage(string.Format(InfraMessages.MinSize, "Name", 3));
        }

        private bool BeAValidDate(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
