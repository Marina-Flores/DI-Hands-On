using DependencyInjection.Repositories.Contracts;
using DependencyInjection.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DependencyInjection.Services.Contracts;
using DependencyInjection.Services;
using RestSharp;

namespace DependencyInjection.Extensions
{
    public static class DependenciesExtensions
    {
        public static void AddSqlConnection(
            this IServiceCollection services, 
            string connectionString)
        {
            services.AddScoped<SqlConnection>(x 
                => new SqlConnection(connectionString));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<ICustomerRepository, CustomerRepository>();
            services.TryAddScoped<IBookRoomRepository, BookRoomRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IPaymentService, PaymentService>();
        }

        public static void AddRestMethods(this IServiceCollection services)
        {
            services.TryAddScoped<RestRequest>();
            services.TryAddScoped<RestClient>();
        }
    }
}
