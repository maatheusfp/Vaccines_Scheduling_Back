using FluentValidation.AspNetCore;
using Vaccines_Scheduling.Validators.Fluent;

namespace Vaccines_Scheduling.Webapi.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<PatientLoginValidator>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<PatientSignUpValidator>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<AppointmentSignUpValidator>());

        }
    }
}
