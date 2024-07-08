using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Vaccines_Scheduling.Configuration;

namespace Vaccines_Scheduling
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddDependencyInjectionConfiguration(Configuracao);

            services.AddDatabaseConfiguration(Configuration);

            //services.AddFluentConfiguration();

            //services.AddAutorizacaoConfiguration(Configuracao);

            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Controle de Tarefas",
                    Version = "v1",
                    Description = "APIs de estudo em dotnet core 6.",
                    Contact = new() { Name = "Danilo Queiroga", Url = new Uri("http://google.com.br") },
                    License = new() { Name = "Private", Url = new Uri("http://google.com.br") },
                    TermsOfService = new Uri("http://google.com.br")
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controle de Tarefas v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<ApiMiddleware>();
            //app.UseMiddleware<UsuarioContextoMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
