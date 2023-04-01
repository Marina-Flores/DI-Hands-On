using DependencyInjection.Attributes;
using DependencyInjection.Entities;
using DependencyInjection.Repositories.Contracts;
using DependencyInjection.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRoomRepository _bookRoomRepository;
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;

        public BookController(
            IConfiguration configuration,
            ICustomerRepository customerRepository, 
            IBookRoomRepository bookRoomRepository,
            IPaymentService paymentService
            )
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
            _bookRoomRepository = bookRoomRepository;
            _paymentService = paymentService;           
        }

       [HttpPost]
       [ApiKey]
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
