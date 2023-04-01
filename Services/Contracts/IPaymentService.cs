using DependencyInjection.Entities;
using RestSharp;

namespace DependencyInjection.Services.Contracts
{
    public interface IPaymentService
    {
        public Task<RestResponse> ProcessPayment(BookRoomCommand command);
    }
}
