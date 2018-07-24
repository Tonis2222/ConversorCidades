using System;
using System.Collections.Generic;

namespace Dominio
{
  public class Cidade
  {
    public string Nome { get; set; }
    public int Habitantes { get; set; }
    public List<Bairro> Bairros { get; set; }
  }
}
