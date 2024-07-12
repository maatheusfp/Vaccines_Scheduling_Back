using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Validator.Fluent
{
    public class PatientSignUpValidator : AbstractValidator<PatientSignUpModel>
    {
        public PatientSignUpValidator() 
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));

            RuleFor(t => t.Login)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MaximumLength(50).WithMessage(string.Format(InfraMessages.MaxSize, "Login", 50));

            RuleFor(t => t.Birthday)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill));

            RuleFor(t => t.Password)
                .NotNull().WithMessage(string.Format(InfraMessages.NeedToFill))
                .NotEmpty().WithMessage(string.Format(InfraMessages.NeedToFill))
                .MinimumLength(5).WithMessage(string.Format(InfraMessages.MinSize, "Password", 5));
        }
    }
}
