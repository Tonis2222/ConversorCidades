using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConversorCidades.Controllers;
using ConversorCidades.DTOs;
using Dominio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositorioMongo;

namespace ConversorCidades
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Secret").Value));
      
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
      })
      .AddJwtBearer("JwtBearer", jwtBearerOptions =>
      {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = sharedKey,
          ValidateIssuer = true,
          ValidIssuer = "Conversor Cidades",
          ValidateAudience = true,
          ValidAudience = "Todos",
          ValidateLifetime = true,
          ClockSkew = TimeSpan.FromMinutes(1),          
         
        };
      });

      services.AddTransient<IRepositorioConfiguracao, RepositorioConfiguracao>(a => new RepositorioConfiguracao(Configuration.GetConnectionString("MongoDB")));
      services.AddTransient<IConfiguracaoToken, ConfiguracaoToken>(a => new ConfiguracaoToken(Configuration.GetSection("Secret").Value));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      Mapper.Initialize(map =>
      {
        map.CreateMap<ConfiguracaoDeConversao, ConfiguracaoDeConversaoDTO>().ReverseMap();
        map.CreateMap<Formato, FormatoDTO>().ReverseMap();
      });

      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
