namespace API.Models;
public class LoginResponse
{
    public string TokenType { get; set; } = string.Empty;
    public string AccessToken { get;set; } = string.Empty;
    public string ExpiresIn { get;set; } = string.Empty;
    public string RefreshToken { get;set; } = string.Empty;
    public bool Success { get;set; }
    public string Message { get;set; } = string.Empty;

    public void RegistrarError(string message)
    {
        this.Success = false;
        this.Message = message;
    }

    public void RegistrarToken(string token)
    {
        this.Success = true;
        this.AccessToken = token;
    }
}
