using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Vaccines_Scheduling.Configuration;
using Vaccines_Scheduling.Webapi.Configuration;
using Vaccines_Scheduling.Webapi.Middleware;

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
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter("yyyy-MM-dd"));
                        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter("HH"));
                        //options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd"));

                    });

            services.AddDependencyInjectionConfiguration(Configuration);
            services.AddDatabaseConfiguration(Configuration);
            services.AddFluentConfiguration();
            services.AddAutorizationConfig(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Projeto Pitang",
                    Version = "v1",
                    Description = "API",
                    Contact = new() { Name = "Matheus Pinheiro", Url = new Uri("http://google.com.br") },
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaccines Scheduling v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ApiMiddleware>();
            app.UseMiddleware<PatientContextMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
