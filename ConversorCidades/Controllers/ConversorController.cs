using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConversorCidades.Controllers
{
  [Produces("application/json")]
  [Route("api/Conversor")]
  public class ConversorController : Controller
  {
    IRepositorioConfiguracao repositorio;

    public ConversorController(IRepositorioConfiguracao _repositorio)
    {
      repositorio = _repositorio;
    }

    // POST: api/Conversor
    [HttpPost("{UF}", Name = "Post")]
    [Authorize]
    public async Task<IActionResult> Post(string UF)
    {
      if (string.IsNullOrEmpty(UF))
        return BadRequest("UF não informada.");

      ConfiguracaoDeConversao configuracao = await repositorio.BuscarConfiguracao(UF);

      if (configuracao == null)
        return BadRequest("Configuração não encontrada.");

      string corpo = string.Empty;

      using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
      {
        corpo = await reader.ReadToEndAsync();
      }

      Conversor c = new Conversor(configuracao);
      string erro = string.Empty;
      List<Cidade> cidades;
      if (c.Converter(corpo, ref erro, out cidades))
      {
        return new ObjectResult(cidades);
      }
      else
      {
        return BadRequest(erro);
      }
    }
  }
}
