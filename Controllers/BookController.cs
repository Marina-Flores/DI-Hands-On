using Dapper;
using DependencyInjection.Entities;
using DependencyInjection.Repositories.Contracts;
using DependencyInjection.Services.Contracts;
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
        private readonly IPaymentService _paymentService;
        public BookController(
            ICustomerRepository customerRepository, 
            IBookRoomRepository bookRoomRepository,
            IPaymentService paymentService)
        {
            _customerRepository = customerRepository;
            _bookRoomRepository = bookRoomRepository;
            _paymentService = paymentService;
        }
        public async Task<IActionResult> Book(BookRoomCommand command)
        {
            var customer = _customerRepository.GetCustomerAsync(command);
            if (customer == null) 
                return NotFound();

            var room = await _bookRoomRepository.GetRoomCommandAsync(command);
            if (room is not null)
                return BadRequest();

            var response = await _paymentService.ProcessPayment(command);

            if (response is null)
                return BadRequest();

            var numReserva = await _bookRoomRepository.InsertBookAsync(new Book(command.Email, command.RoomId, command.Day));           
            return Ok(numReserva);
        }

    }

    
    }
