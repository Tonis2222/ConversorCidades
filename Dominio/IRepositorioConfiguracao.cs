using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
  public interface IRepositorioConfiguracao
  {
    Task SalvarConfiguracao(ConfiguracaoDeConversao configuracao);

    Task AtualizarConfiguracao(ConfiguracaoDeConversao configuracao);
    
    Task<List<ConfiguracaoDeConversao>> ListarConfiguracao();
    Task<ConfiguracaoDeConversao> BuscarConfiguracao(string UF);
  }
}
