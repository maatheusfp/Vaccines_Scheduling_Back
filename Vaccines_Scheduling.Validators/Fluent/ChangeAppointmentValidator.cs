using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validators.Fluent
{
    public class ChangeAppointmentValidator : AbstractValidator<AppointmentChangeModel>
    {
        public ChangeAppointmentValidator()
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage(InfraMessages.NeedToFill)
                .NotEmpty().WithMessage(InfraMessages.NeedToFill);

            RuleFor(x => x.Time)
                .NotNull().WithMessage(InfraMessages.NeedToFill)
                .NotEmpty().WithMessage(InfraMessages.NeedToFill);

            RuleFor(x => x.Status).
                NotNull().WithMessage(InfraMessages.NeedToFill).
                NotEmpty().WithMessage(InfraMessages.NeedToFill);
        }
    }
}
