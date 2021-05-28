using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TbNeo.Domain.Core.Data
{
    public abstract class ConnectionBase
    {
        private IConfiguration _configuration;

        protected ConnectionBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
