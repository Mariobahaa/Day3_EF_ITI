using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DP.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace DP.EnitiyManagers
{
    public class PublisherManager
    {
        IDbConnection Conn = new SqlConnection("Data Source=.; Initial Catalog=pubs; Integrated Security=true;");
        public List<Publisher> selectAllPublishers()
        {
            return Conn.Query<Publisher>("selectAllPublishers", commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
