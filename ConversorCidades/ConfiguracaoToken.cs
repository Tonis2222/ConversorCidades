using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversorCidades
{
  public class ConfiguracaoToken : IConfiguracaoToken
  {
    string chave;

    public ConfiguracaoToken(string _chave)
    {
      chave = _chave;
    }

    public string Chave { get { return chave; } }

  }
}
