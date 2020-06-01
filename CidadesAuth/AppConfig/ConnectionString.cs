using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CidadesAuth.AppConfig
{
    //Classe usada para disponibilizar a string de conexão ao banco de dados
    //para o resto da aplicacao por meio de dependency injection.
    //A string de conexao esta em appsettings.json.
    public class ConnectionString
    {
        public string Value { get; }

        public ConnectionString(string value)
        {
            Value = value;
        }
    }
}
