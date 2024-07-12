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
    public class PatientLoginValidator : AbstractValidator<PatientLoginModel>
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
