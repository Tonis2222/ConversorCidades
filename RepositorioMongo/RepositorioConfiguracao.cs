using Dominio;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositorioMongo
{
  public class RepositorioConfiguracao : IRepositorioConfiguracao
  {
    private const string NOME_BASE_CONFIGURACAO = "configuracao";
    IMongoClient clienteMongoDB;
    IMongoDatabase baseConfiguracao;
    IMongoCollection<ConfiguracaoDeConversao> configuracoes;


    public RepositorioConfiguracao(string connectionString)
    {
      this.clienteMongoDB = new MongoClient(connectionString);

      baseConfiguracao = clienteMongoDB.GetDatabase(NOME_BASE_CONFIGURACAO);

      configuracoes = baseConfiguracao.GetCollection<ConfiguracaoDeConversao>(nameof(ConfiguracaoDeConversao));

      if (configuracoes == null)
      {
        baseConfiguracao.CreateCollection(nameof(ConfiguracaoDeConversao));

        configuracoes = baseConfiguracao.GetCollection<ConfiguracaoDeConversao>(nameof(ConfiguracaoDeConversao));
      }
    }

    public async Task AtualizarConfiguracao(ConfiguracaoDeConversao configuracao)
    {
      await configuracoes.ReplaceOneAsync(Builders<ConfiguracaoDeConversao>.Filter.Eq(a => a.UF, configuracao.UF), configuracao);
    }
    
    public async Task<List<ConfiguracaoDeConversao>> ListarConfiguracao()
    {
      var retorno = await configuracoes.FindAsync(Builders<ConfiguracaoDeConversao>.Filter.Empty);
      return retorno.ToList();
    }

    public async Task SalvarConfiguracao(ConfiguracaoDeConversao configuracao)
    {
      await configuracoes.InsertOneAsync(configuracao);
    }

    public async Task ExcluirConfiguracao(ConfiguracaoDeConversao configuracao)
    {
      await configuracoes.DeleteOneAsync(Builders<ConfiguracaoDeConversao>.Filter.Eq(a => a.UF, configuracao.UF));
    }

    public async Task<ConfiguracaoDeConversao> BuscarConfiguracao(string UF)
    {
      var retorno = await configuracoes.FindAsync(Builders<ConfiguracaoDeConversao>.Filter.Eq(a => a.UF, UF));
      return retorno.FirstOrDefault();
    }
  }
}
