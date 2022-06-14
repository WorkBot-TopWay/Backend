namespace TopWay.API.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string UrlPhoto { get; set; }
    public string Token { get; set; }
}