using Vaccines_Scheduling.Utility.Configurations;
using Vaccines_Scheduling.Utility.PatientContext;

namespace Vaccines_Scheduling.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuracao)
        {
            InjectRepository(services);
            InjectBusiness(services);
            //InjectMiddleware(services);

            //services.AddScoped<IGerenciadorTransacao, GerenciadorTransacao>();
            services.AddScoped<IPatientContext, PatientContext>();
            services.AddOptions<AuthConfig>().Bind(configuracao.GetSection("Authorization"));
        }

        //private static void InjectMiddleware(IServiceCollection services)
        //{
        //    services.AddTransient<ApiMiddleware>();
        //    services.AddTransient<UsuarioContextoMiddleware>();
        //}

        private static void InjectBusiness(IServiceCollection services)
        {
            //services.AddScoped<ITarefaNegocio, TarefaNegocio>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            //services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            //services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            //services.AddScoped<ITarefaUsuarioRepositorio, TarefaUsuarioRepositorio>();
        }
    }
}

