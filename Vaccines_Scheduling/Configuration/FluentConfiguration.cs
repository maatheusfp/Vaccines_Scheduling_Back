using FluentValidation.AspNetCore;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Webapi.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<PatientLoginModel>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<PatientSignUpModel>());
            services.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<AppointmentSignUpModel>());
        }
    }
}
