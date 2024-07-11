using Vaccines_Scheduling.Business.Businesses;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Repository.Repositories;
using Vaccines_Scheduling.Utility.Configurations;
using Vaccines_Scheduling.Utility.PatientContext;
using Vaccines_Scheduling.Webapi.Middleware;

namespace Vaccines_Scheduling.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectRepository(services);
            InjectBusiness(services);
            InjectMiddleware(services);

            //services.AddScoped<IGerenciadorTransacao, GerenciadorTransacao>();
            services.AddScoped<IPatientContext, PatientContext>();
            services.AddOptions<AuthConfig>().Bind(configuration.GetSection("Authorization"));
        }

        private static void InjectMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
            services.AddTransient<PatientContextMiddleware>();
        }

        private static void InjectBusiness(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
            services.AddScoped<IPatientSignUpBusiness, PatientSignUpBusiness>();
            services.AddScoped<IAppointmentSignUpBusiness, AppointmentSignUpBusiness>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IPatientSignUpRepository, PatientSignUpRepository>();
            services.AddScoped<IAppointmentSignUpRepository, AppointmentSignUpRepository>();
        }
    }
}

