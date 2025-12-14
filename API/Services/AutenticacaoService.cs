using API.Models;

namespace API.Services;

public class AutenticacaoService(JwtTokenGenerator jwtTokenGenerator)
{
    private readonly JwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<LoginResponse> LoginAsync(string password)
    {
        var response = new LoginResponse();

        if (password == "8318")
        {
            var token = _jwtTokenGenerator.Execute("Tiago");
            response.RegistrarToken(token);
        }
        else
            response.RegistrarError("Login ou senha incorretos.");

        return await Task.FromResult(response);
    }
}
