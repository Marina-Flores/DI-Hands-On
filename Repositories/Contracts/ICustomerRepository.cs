using DependencyInjection.Entities;

namespace DependencyInjection.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(BookRoomCommand command);
    }
}
