using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FunOlympicDataManager.Library.DataAccess.Internal;
public class SqlDataAccess : ISqlDataAccess
{
    public IConfiguration Configuration { get; }
    public SqlDataAccess(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GetConnectionString(string connectionStringName)
    {
        var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString(connectionStringName));
        builder.Password = "GoldenSky62";
        builder.UserID = "OmsGold";
        return builder.ToString();
    }
    public List<T> LoadData<T, U>(string storeProcedure, U parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(storeProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();
            return rows;
        }
    }

    public int SaveData<T>(string storeProcedure, T parameters, string connectionStringName)
    {
        int id = 0;
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var ret= connection.Execute(storeProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            if (ret != null)
                id = int.Parse(ret.ToString());
        }
        return id;
    }

    public int SaveDataQ<T>(string commantText, T parameters, string connectionStringName)
    {
        int id=0;
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var ret = connection.Execute(commantText, parameters, commandType: CommandType.Text);
            if(ret!=null)
                id = int.Parse(ret.ToString());
        }
        return id;
    }
    public T LoadFirstData<T, U>(string commandText, U parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<T>(commandText, parameters, commandType: CommandType.Text);
        }
    }
    public List<T> LoadDataq<T, U>(string commandText, U parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(commandText, parameters, commandType: CommandType.Text).ToList();
            return rows;
        }
    }
}
