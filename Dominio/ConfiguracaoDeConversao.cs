using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dominio
{
  public class ConfiguracaoDeConversao
  {
    //estou tendo problemas com o mongo, precisei add um id
    public object Id { get; set; }

    public string UF { get; set; }
    public Formato Formato { get; set; }
    public string CaminhoCidades { get; set; }
    public string CaminhoNomeCidades { get; set; }
    public string CaminhoHabitantesCidades { get; set; }
    public string CaminhoBairros { get; set; }
    public string CaminhoNomeBairros { get; set; }
    public string CaminhoHabitantesBairros { get; set; }

    internal Queue<string> PegaCaminhoCidades()
    {
      return PegaCaminhos(CaminhoCidades);
    }

    internal Queue<string> PegaCaminhoBairros()
    {
      return PegaCaminhos(CaminhoBairros);
    }

    public bool Validar(out string erro)
    {
      erro = string.Empty;

      if (string.IsNullOrEmpty(CaminhoCidades))
      {
        erro = "Caminho cidade não informado.";
        return false;
      }

      if (string.IsNullOrEmpty(CaminhoBairros))
      {
        erro = "Caminho bairro não informado.";
        return false;
      }

      if (string.IsNullOrEmpty(UF))
      {
        erro = "UF não informado.";
        return false;
      }

      if (string.IsNullOrEmpty(CaminhoNomeCidades))
      {
        erro = "Caminho nome cidade não informado.";
        return false;
      }
      if (string.IsNullOrEmpty(CaminhoNomeBairros))
      {
        erro = "Caminho nome bairro não informado.";
        return false;
      }
      if (string.IsNullOrEmpty(CaminhoHabitantesBairros))
      {
        erro = "Caminho habitantes bairro não informado.";
        return false;
      }
      if (string.IsNullOrEmpty(CaminhoHabitantesCidades))
      {
        erro = "Caminho habitantes cidade não informado.";
        return false;
      }
      if (!Formato.IsDefined(typeof(Formato),Formato))
      {
        erro = "Formato não informado.";
        return false;
      }

      return true;
    }

    internal Queue<string> PegaCaminhoHabitantesBairro()
    {
      return PegaCaminhos(CaminhoHabitantesBairros);
    }

    internal Queue<string> PegaCaminhoNomeBairro()
    {
      return PegaCaminhos(CaminhoNomeBairros);
    }

    internal Queue<string> PegaCaminhoNomeCidade()
    {
      return PegaCaminhos(CaminhoNomeCidades);
    }

    internal Queue<string> PegaCaminhoHabitantesCidade()
    {
      return PegaCaminhos(CaminhoHabitantesCidades);
    }

    private Queue<string> PegaCaminhos(string caminhoCompleto)
    {
      Queue<string> caminhos = new Queue<string>();
      var partes = caminhoCompleto.Split(".").ToList();
      partes.ForEach(a => caminhos.Enqueue(a));
      return caminhos;
    }
  }
}
