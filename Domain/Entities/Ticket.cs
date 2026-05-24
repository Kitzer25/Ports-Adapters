namespace Domain.Entities;

public enum TicketStatus
{
    Abierto,
    EnProceso,
    Cerrado
}

public class Ticket
{
    private readonly List<Response> _responses = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TicketStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    public IReadOnlyCollection<Response> Responses => _responses;

    public Ticket(Guid id, Guid userId, string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título requerido");

        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        Status = TicketStatus.Abierto;
        CreatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        if (Status == TicketStatus.Cerrado)
            throw new InvalidOperationException("Ya está cerrado");

        Status = TicketStatus.Cerrado;
        ClosedAt = DateTime.UtcNow;
    }

    public void AddResponse(Response response)
    {
        if (Status == TicketStatus.Cerrado)
            throw new InvalidOperationException("No puedes responder un ticket cerrado");

        _responses.Add(response);
        Status = TicketStatus.EnProceso;
    }
}