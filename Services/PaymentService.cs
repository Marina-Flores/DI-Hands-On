using DependencyInjection.Entities;
using DependencyInjection.Services.Contracts;
using RestSharp;

namespace DependencyInjection.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _client;
        private readonly RestRequest _request;

        public PaymentService(
            IConfiguration configuration,
            RestClient client, 
            RestRequest request) 
        {
            _configuration = configuration;
            _client = client;   
            _request = request;
        }
        public async Task<RestResponse> ProcessPayment(BookRoomCommand command)
        {
            var apiKeyName = "PaymentApiKey";

            var request = _request
                    .AddQueryParameter(apiKeyName, _configuration.GetValue<string>(apiKeyName))
                    .AddJsonBody(new
                    {
                        User = command.Email,
                        CreditCard = command.CreditCard
                    });

            return await _client.PostAsync(request, new CancellationToken());
        }
    }
}
