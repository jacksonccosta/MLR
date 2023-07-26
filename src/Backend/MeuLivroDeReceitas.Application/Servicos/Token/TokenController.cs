using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application;

public class TokenController
{
    private const string EmailAlias = "mla";
    private readonly double _tempoToken;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoToken, string chaveDeSeguranca)
    {
        _tempoToken = tempoToken;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(string emailUsuario)
    {
        var claims = new List<Claim> 
        {
            new Claim(EmailAlias, emailUsuario),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoToken),
            SigningCredentials = new SigningCredentials(SimmetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public void ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var paramValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimmetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        tokenHandler.ValidateToken(token, paramValidacao, out _);
    }

    private SymmetricSecurityKey SimmetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }
}
