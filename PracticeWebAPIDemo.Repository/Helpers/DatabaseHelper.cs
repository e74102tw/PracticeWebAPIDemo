using CoreProfiler;
using CoreProfiler.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PracticeWebAPIDemo.Repository.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            var Connection = new ProfiledDbConnection
            (
                new SqlConnection(this._connectionString),
                () => ProfilingSession.Current == null ?
                    null :
                    new DbProfiler(ProfilingSession.Current.Profiler)
            );

            return Connection;
        }
    }
}
