using DependencyInjection.Extensions;
using DependencyInjection.Repositories;
using DependencyInjection.Repositories.Contracts;
using DependencyInjection.Services;
using DependencyInjection.Services.Contracts;
using Microsoft.Data.SqlClient;
using RestSharp;

namespace DependencyInjection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connStr = Configuration.GetConnectionString("DefaultConnection") ?? 
                  throw new InvalidOperationException("A configuração não foi encontrada");

            services.AddControllers();

            services.AddSqlConnection(connStr);
            services.AddServices();
            services.AddRepositories();
            services.AddRestMethods();

            services.AddSingleton(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();                

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());            
        }
    }
}