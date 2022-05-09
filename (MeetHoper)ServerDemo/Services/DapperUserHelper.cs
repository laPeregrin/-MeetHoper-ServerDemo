using Common.Models.Responses;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class DapperUserHelper
    {
        private readonly string _connectionString;

        public DapperUserHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevelopConnection");
        }

        public async Task<IEnumerable<UserPublicDataResponse>> GetFromDBByIds(Guid[] ids)
        {
            try
            {
                using (var con = GetSqlConnection())
                {
                    return await con.QueryAsync<UserPublicDataResponse>("select * from dbo.Users where Id in @Ids", new { Ids = ids });
                }
            }
            catch(Exception)
            {
                return Enumerable.Empty<UserPublicDataResponse>();
            }
        }

        private SqlConnection GetSqlConnection() =>
           new SqlConnection(_connectionString);

    }
}
