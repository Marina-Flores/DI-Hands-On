using DependencyInjection.Entities;
using DependencyInjection.Services.Contracts;
using RestSharp;

namespace DependencyInjection.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;

        public PaymentService(RestClient client, RestRequest request) 
        {
            _client = client;   
            _request = request;
        }
        public async Task<RestResponse> ProcessPayment(BookRoomCommand command)
        {
            var request = _request
                    .AddQueryParameter("api_key", "c20c8acb-bd76-4597-ac89-10fd955ac60d")
                    .AddJsonBody(new
                    {
                        User = command.Email,
                        CreditCard = command.CreditCard
                    });

            return await _client.PostAsync(request, new CancellationToken());

        }
    }
}
