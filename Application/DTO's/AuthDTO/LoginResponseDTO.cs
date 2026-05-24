namespace Domain.DTO_s.AuthDTO;

public class LoginResponseDTO
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Username { get; set; }
}