using Dapper;
using DependencyInjection.Entities;
using DependencyInjection.Repositories.Contracts;
using Microsoft.Data.SqlClient;

namespace DependencyInjection.Repositories
{
    public class BookRoomRepository : IBookRoomRepository
    {
        private readonly SqlConnection _connection;
        public BookRoomRepository(SqlConnection sqlConnection) 
           => _connection = sqlConnection;  
        public async Task<Book> GetRoomCommandAsync(BookRoomCommand command)
        {
            return await _connection.QueryFirstOrDefaultAsync<Book>(
                "SELECT * FROM [Book] WHERE [Room] = @room AND [Date] BETWEEN @dateStart AND @dateEnd",
                new
                {
                    Room = command.RoomId,
                    DateStart = command.Day.Date,
                    DateEnd = command.Day.Date.AddDays(1).AddTicks(-1)
                });
        }
    }
}
