using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConversorCidades.DTOs;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConversorCidades.Controllers
{
  [Route("api/[controller]")]
  public class ConfiguracoesController : Controller
  {
    IRepositorioConfiguracao repositorio;

    public ConfiguracoesController(IRepositorioConfiguracao _repositorio)
    {
      repositorio = _repositorio;
    }

    // GET api/values
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
      var configuracoes = await repositorio.ListarConfiguracao();
      var configuracoesDTO = configuracoes.Select(a => Mapper.Map<ConfiguracaoDeConversaoDTO>(a));
      return new ObjectResult(configuracoesDTO);
    }

    // POST api/values
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody]ConfiguracaoDeConversaoDTO value)
    {
      string erro;
      var configuracao = Mapper.Map<ConfiguracaoDeConversao>(value);
      configuracao.Id = Guid.NewGuid();
      if (!configuracao.Validar(out erro))
      {
        return BadRequest(erro);
      }
      await repositorio.SalvarConfiguracao(configuracao);
      return Ok();
    }

    // PUT api/values/5
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody]ConfiguracaoDeConversaoDTO value)
    {
      string erro;
      var configuracao = Mapper.Map<ConfiguracaoDeConversao>(value);
      if (!configuracao.Validar(out erro))
      {
        return BadRequest(erro);
      }
      await repositorio.AtualizarConfiguracao(configuracao);
      return Ok();
    }

  }
}
