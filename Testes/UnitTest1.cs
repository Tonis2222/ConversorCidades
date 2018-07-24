using Dominio;
using System;
using System.Collections.Generic;
using Xunit;

namespace Testes
{
  public class UnitTest1
  {
  
    [Fact]
    public void TesteinsertMG()
    {
      Conversor conversor = new Conversor(new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "body.region.%.cities.city.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.neighborhood.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitentesBairros = "population",
        UF = "MG"
      });

      string conteudo = @"<body>
<region>
<name>Triangulo Mineiro</name>
<cities>
<city>
<name>Uberlandia</name>
<population>700001</population>
<neighborhoods>
<neighborhood>
<name>Santa Monica</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
</neighborhoods>
</city>
<city>
<name>BH</name>
<population>700001</population>
<neighborhoods>
<neighborhood>
<name>Santa Monica</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
</neighborhoods>
</city>
</cities>
</region>

<region>
<name>Estrada Real</name>
<cities>
<city>
<name>Ouro Preto</name>
<population>700001</population>
<neighborhoods>
<neighborhood>
<name>Santa Monica</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
</neighborhoods>
</city>
<city>
<name>Tiradentes</name>
<population>700001</population>
<neighborhoods>
<neighborhood>
<name>Santa Monica</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
<neighborhood>
<name>Santa Monica2</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
<neighborhood>
<name>Santa Monica3</name>
<zone>Zona Leste</zone>
<population>13012</population>
</neighborhood>
</neighborhoods>
</city>
</cities>
</region>
</body>";

      string erro = string.Empty;

      List<Cidade> cidades;

      Assert.True(conversor.Converter(conteudo, ref erro, out cidades));


    }
    [Fact]
    public void TesteInsertRJ()
    {
      Conversor conversor = new Conversor(new ConfiguracaoDeConversao()
      {
        Formato = Formato.XML,
        CaminhoCidades = "corpo.cidade.%",
        CaminhoNomeCidades = "nome",
        CaminhoHabitantesCidades = "populacao",
        CaminhoBairros = "bairros.bairro.%",
        CaminhoNomeBairros = "nome",
        CaminhoHabitentesBairros = "populacao",
        UF = "RJ"
      });

      string conteudo = @"<corpo>
<cidade>
<nome>Rio de Janeiro</nome>
<populacao>10345678</populacao>
<bairros>
<bairro>
<nome>Tijuca</nome>
<regiao>Zona Norte</regiao>
<populacao>135678</populacao>
</bairro>
<bairro>
<nome>Botafogo</nome>
<regiao>Zona Sul</regiao>
<populacao>105711</populacao>
</bairro>
</bairros>
</cidade>
</corpo>";

      string erro = string.Empty;

      List<Cidade> cidades;

      Assert.True(conversor.Converter(conteudo, ref erro, out cidades));


    }
    [Fact]
    public void TesteInsertAC()
    {
      Conversor conversor = new Conversor(new ConfiguracaoDeConversao()
      {
        Formato = Formato.Json,
        CaminhoCidades = "cities.%",
        CaminhoNomeCidades = "name",
        CaminhoHabitantesCidades = "population",
        CaminhoBairros = "neighborhoods.%",
        CaminhoNomeBairros = "name",
        CaminhoHabitentesBairros = "population",
        UF = "AC"
      });

      string conteudo = "{\"cities\":[{\"name\":\"Rio Branco\",\"population\":576589,\"neighborhoods\":[{\"name\":\"Habitasa\",\"population\":7503}]}]}";

      string erro = string.Empty;

      List<Cidade> cidades;

      Assert.True(conversor.Converter(conteudo, ref erro, out cidades));


    }
  }
}
