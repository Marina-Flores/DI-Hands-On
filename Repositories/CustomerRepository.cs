using Dapper;
using DependencyInjection.Entities;
using DependencyInjection.Repositories.Contracts;
using Microsoft.Data.SqlClient;

namespace DependencyInjection.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _conn;
        public CustomerRepository(SqlConnection conn) 
            => _conn = conn;

        public async Task<Customer> GetCustomerAsync(BookRoomCommand command)
        {
            return await _conn
                   .QueryFirstOrDefaultAsync<Customer>("SELECT * FROM [Customer] WHERE [Email] = @email",
                   new { command.Email });
        }
    }
}
