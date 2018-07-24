using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversorCidades.DTOs
{
  public class ConfiguracaoDeConversaoDTO
  {
    public string UF { get; set; }
    public FormatoDTO Formato { get; set; }
    public string CaminhoCidades { get; set; }
    public string CaminhoNomeCidades { get; set; }
    public string CaminhoHabitantesCidades { get; set; }
    public string CaminhoBairros { get; set; }
    public string CaminhoNomeBairros { get; set; }
    public string CaminhoHabitentesBairros { get; set; }
  }
}
