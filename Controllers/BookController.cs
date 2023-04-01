using Dapper;
using DependencyInjection.Entities;
using DependencyInjection.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestSharp;

namespace DependencyInjection.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRoomRepository _bookRoomRepository;

        public BookController(
            ICustomerRepository customerRepository, 
            IBookRoomRepository bookRoomRepository)
        {
            _customerRepository = customerRepository;
            _bookRoomRepository = bookRoomRepository;
        }
        public async Task<IActionResult> Book(BookRoomCommand command)
        {
            // Recupera o usuário
            var customer = _customerRepository.GetCustomerAsync(command);

            if (customer == null) 
                return NotFound();

            // Verifica se a sala está disponível 
            var room = await _bookRoomRepository.GetRoomCommandAsync(command);

            // Se existe uma reserva, a sala está indisponível
            if (room is not null)
                return BadRequest();

            // Tenta fazer um pagamento
            var client = new RestClient("https://payments.com");
            var request = new RestRequest()
                .AddQueryParameter("api_key", "c20c8acb-bd76-4597-ac89-10fd955ac60d")
                .AddJsonBody(new
                {
                    User = command.Email,
                    CreditCard = command.CreditCard
                });

            var response = await client.PostAsync(request, new CancellationToken());

            // Se a requisição não pode ser completa
            if (response is null)
                return BadRequest();

            // Cria a reserva
            var book = new Book(command.Email, command.RoomId, command.Day);

            // Salva os dados 
            await connection.ExecuteAsync("INSERT INTO [Book] VALUES (@date, @email, @room)", new
            {
                book.Date,
                book.Email,
                book.Room
            });

            // Retorna o número da reserva
            return Ok();

        }

    }

    
    }
