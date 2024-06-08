using BitzArt.Blazor.Auth;
using Dominio.Auth;

namespace NewApp.Services;

public class SampleServerSideAuthenticationService(JwtService jwtService) : ServerSideAuthenticationService<SignIn, SignUp>
{
    private readonly JwtService _jwtService = jwtService;

    protected override Task<AuthenticationResult> GetSignInResultAsync(SignIn signIn)
    {
        if (signIn.Password == "8318")
            return Task.FromResult(AuthenticationResult.Success(_jwtService.BuildJwtPair("Tiago")));
        else
            return Task.FromResult(AuthenticationResult.Failure("Contraseña incorrecta."));
    }

    protected override Task<AuthenticationResult> GetSignUpResultAsync(SignUp signUp)
    {
        return Task.FromResult(AuthenticationResult.Failure("INDISPONÍVEL"));
    }

    public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
    {
        var authResult = AuthenticationResult.Success(_jwtService.BuildJwtPair("Tiago"));
        return Task.FromResult(authResult);
    }
}
