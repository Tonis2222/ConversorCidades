using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testes
{
  public class ConfiguracaoDeConversaoTest
  {

    [Fact]
    public void ConfiguracaoSemCaminhoCidadeDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho cidade não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemCaminhoBairroDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho bairro não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemUFDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "UF não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemCaminhoNomeCidadeDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho nome cidade não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemCaminhoNomeBairroDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho nome bairro não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemCaminhoHabitantesBairroDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho habitantes bairro não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemCaminhoHabitantesCidadeDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Caminho habitantes cidade não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }

    [Fact]
    public void ConfiguracaoSemFormatoArquivoDaErro()
    {
      bool resultadoValidacaoEsperado = false;
      bool resultadoValidacao;
      string erroEsperado = "Formato não informado.";
      string erro = string.Empty;

      var config = new ConfiguracaoDeConversao()
      {
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitantesBairros = "population",
        UF = "MG"
      };

      resultadoValidacao = config.Validar(out erro);
      Assert.Equal(resultadoValidacaoEsperado, resultadoValidacao);
      Assert.Equal(erroEsperado, erro);
    }




  }
}
