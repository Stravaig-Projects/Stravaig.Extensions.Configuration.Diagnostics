using System.Data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.__fakes
{
    public class FakeDbConnection : IDbConnection
    {
        public FakeDbConnection()
        {
            State = ConnectionState.Closed;
        }
        public void Dispose()
        {
        }

        public IDbTransaction BeginTransaction()
        {
            return null;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return null;
        }

        public void ChangeDatabase(string databaseName)
        {
        }

        public void Close()
        {
        }

        public IDbCommand CreateCommand()
        {
            return null;
        }

        public void Open()
        {
            State = ConnectionState.Open;
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; } = 15;
        public string Database { get; } = string.Empty;
        public ConnectionState State { get; private set; }

        public FakeDbConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}