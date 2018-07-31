using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Testes
{
  public class MongoTest
  {

    private const string connectionString = "mongodb://localhost:27017";

    [Fact]
    public void InsertConfiguracao()
    {
      RepositorioMongo.RepositorioConfiguracao rep = new RepositorioMongo.RepositorioConfiguracao(connectionString);

      var ret = rep.ListarConfiguracao();
      ret.Wait();

      foreach (var config in ret.Result)
      {
        rep.ExcluirConfiguracao(config).Wait();
      }

      var configInsert = new ConfiguracaoDeConversao()
      {
        Id = Guid.NewGuid(),
        Formato = Formato.XML,
        CaminhoCidades = "body/%region/cities/%city",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods/%neighborhood",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "TST"
      };

      var configInsert2 = new ConfiguracaoDeConversao()
      {
        Id = Guid.NewGuid(),
        Formato = Formato.XML,
        CaminhoCidades = "body/%region/cities/%city",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods/%neighborhood",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "TST2"
      };

      rep.SalvarConfiguracao(configInsert).Wait();
      rep.SalvarConfiguracao(configInsert2).Wait();


      ret = rep.ListarConfiguracao();
      ret.Wait();
      Assert.Contains(ret.Result, a => a.UF == "TST");
      Assert.Contains(ret.Result, a => a.UF == "TST2");

      configInsert2.Formato = Formato.Json;

      rep.AtualizarConfiguracao(configInsert2).Wait();

      ret = rep.ListarConfiguracao();
      ret.Wait();
      Assert.Contains(ret.Result, a => a.UF == "TST");
      Assert.Contains(ret.Result, a => a.UF == "TST2");
      Assert.True(ret.Result.First(a => a.UF == "TST2").Formato == Formato.Json);

      rep.ExcluirConfiguracao(configInsert).Wait();

      ret = rep.ListarConfiguracao();
      ret.Wait();
      Assert.DoesNotContain(ret.Result, a => a.UF == "TST");
      Assert.Contains(ret.Result, a => a.UF == "TST2");

    }
  }
}
