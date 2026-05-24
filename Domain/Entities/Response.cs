namespace Domain.Entities;

public class Response
{
    public Guid Id { get; private set; }
    public Guid TicketId { get; private set; }
    public Guid ResponderId { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Response(Guid id, Guid ticketId, Guid responderId, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Mensaje requerido");

        Id = id;
        TicketId = ticketId;
        ResponderId = responderId;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }
}