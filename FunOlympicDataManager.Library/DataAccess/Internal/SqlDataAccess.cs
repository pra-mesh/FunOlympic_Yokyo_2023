using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

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

    public void SaveData<T>(string storeProcedure, T parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(storeProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }
    }

    public void SaveDataQ<T>(string commantText, T parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var ret = connection.ExecuteScalar(commantText, parameters, commandType: CommandType.Text);
        }
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
