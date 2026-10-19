using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Repositorio
{
    public class VeterinariaDbContext
    {
        static VeterinariaDbContext()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(DbContextConfig.ConnectionString);
        }
    }
}
