using Dynamitey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Dominio
{
  public class Conversor
  {
    private readonly ConfiguracaoDeConversao configuracao;

    public Conversor(ConfiguracaoDeConversao _configuracao)
    {
      configuracao = _configuracao;
    }
        
    public bool Converter(string conteudo, ref string erro, out List<Cidade> cidades)
    {
      cidades = new List<Cidade>();

      JObject objetoConvertido;
      
      switch (configuracao.Formato)
      {
        case Formato.XML:

          if (!ConverterDeXML(conteudo, out objetoConvertido))
          {
            erro = "Formato inválido";
            return false;
          }

          break;
        case Formato.Json:

          if (!ConverterDeJson(conteudo, out objetoConvertido))
          {
            erro = "Formato inválido";
            return false;
          }

          break;
        default:
          erro = "Formato desconhecido";
          return false;
      }

      try
      {

        var lista = new List<object>();
        BuscarListaNoObjeto(objetoConvertido, configuracao.PegaCaminhoCidades(), ref lista);


        foreach (var dCidade in lista)
        {

          Cidade c = new Cidade();

          c.Nome = BuscarPropriedadeNoObjeto(dCidade, configuracao.PegaCaminhoNomeCidade());
          c.Habitantes = int.Parse(BuscarPropriedadeNoObjeto(dCidade, configuracao.PegaCaminhoHabitantesCidade()));

          c.Bairros = new List<Bairro>();

          var listaBairros = new List<object>();
          BuscarListaNoObjeto(dCidade, configuracao.PegaCaminhoBairros(), ref listaBairros);

          foreach (var dBairro in listaBairros)
          {
            Bairro b = new Bairro();

            b.Nome = BuscarPropriedadeNoObjeto(dBairro, configuracao.PegaCaminhoNomeBairro());
            b.Habitantes = int.Parse(BuscarPropriedadeNoObjeto(dBairro, configuracao.PegaCaminhoHabitantesBairro()));

            c.Bairros.Add(b);

          }

          cidades.Add(c);

        }
      }
      catch (Exception)
      {
        erro = "Configuração inválida.";
        return false;
      }

      return true;
    }

    public void BuscarListaNoObjeto(object objeto, Queue<string> caminhos, ref List<object> retorno)
    {
      string caminho;
      if (caminhos.TryDequeue(out caminho))
      {
        if (objeto is JArray)
        {
          foreach (object item in (objeto as JArray))
          {
            BuscarListaNoObjeto(item, ClonarFila(caminhos), ref retorno);
          }
        }
        else if (caminho == "%")
        {
          BuscarListaNoObjeto(objeto, ClonarFila(caminhos), ref retorno);
        }
        else
        {
          BuscarListaNoObjeto(Dynamic.InvokeGet(objeto, caminho), ClonarFila(caminhos), ref retorno);
        }
      }
      else
      {
        retorno.Add(objeto);
      }
    }

    private string BuscarPropriedadeNoObjeto(dynamic objeto, Queue<string> caminhos)
    {
      string caminho;
      if (caminhos.TryDequeue(out caminho))
      {
        return BuscarPropriedadeNoObjeto(Dynamic.InvokeGet(objeto, caminho), caminhos);
      }
      else
      {
        return objeto.Value.ToString();
      }
    }

    private Queue<string> ClonarFila(Queue<string> caminhos)
    {
      return new Queue<string>(caminhos);
    }

    private bool ConverterDeXML(string conteudo, out JObject objeto)
    {
      try
      {
        string json = JsonConvert.SerializeXNode(XDocument.Parse(conteudo));
        return ConverterDeJson(json, out objeto);
      }
      catch (Exception)
      {
        objeto = null;
        return false;
      }
    }

    private bool ConverterDeJson(string conteudo, out JObject objeto)
    {
      try
      {
        objeto = JObject.Parse(conteudo);
        return true;
      }
      catch (Exception ex)
      {
        objeto = null;
        return false;
      }
    }
  }
}
