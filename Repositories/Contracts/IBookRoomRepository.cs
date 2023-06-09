﻿using DependencyInjection.Entities;

namespace DependencyInjection.Repositories.Contracts
{
    public interface IBookRoomRepository
    {
        Task<Book> GetRoomCommandAsync(BookRoomCommand command);
        Task<Guid> InsertBookAsync(Book book);
    }
}
