namespace DependencyInjection.Entities
{
    public class BookRoomCommand
    {
        public string Email { get; set; }
        public Guid RoomId { get; set; }
        public DateTime Day { get; set; }
        public CreditCard? CreditCard { get; set; }
    }

    public record PaymentResponse(int Code, string Status);
    public record Customer(string Email);
    public record Room(Guid Id, string Name);
    public record Book(string Email, Guid Room, DateTime Date);
    public record CreditCard(string Name, string Holder, string Expiration, string Cvv);
}
