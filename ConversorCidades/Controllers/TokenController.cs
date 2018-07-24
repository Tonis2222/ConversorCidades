using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ConversorCidades.Controllers
{
  [Produces("application/json")]
  [Route("api/Token")]
  public class TokenController : Controller
  {
    IConfiguracaoToken configuracaoToken;

    public TokenController(IConfiguracaoToken _configuracaoToken)
    {
      configuracaoToken = _configuracaoToken;
    }
    
    [HttpGet]
    public IActionResult GetPost()
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracaoToken.Chave));

      var claims = new Claim[] { new Claim(ClaimTypes.Name, "Nada") };

      var token = new JwtSecurityToken(
        
        issuer: "Conversor Cidades",
        audience: "Todos",
        claims: claims,
        notBefore: DateTime.Now,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
      );

      return new ObjectResult(token);
    }

  }
}
