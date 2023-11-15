using System.Data;
using System.Data.SqlClient;
using StackFlow.Shared;

namespace StackFlow.Infra.DataContexts
{
  public class StackFlowDataContext : IDisposable
  {
    public SqlConnection Connection { get; set; }

    public StackFlowDataContext()
    {
      Connection = new SqlConnection(Settings.ConnectionString);
      Connection.Open();
    }

    public void Dispose()
    {
      if (Connection.State != ConnectionState.Closed)
        Connection.Close();
    }
  }
}