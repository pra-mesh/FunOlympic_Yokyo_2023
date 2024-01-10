using Microsoft.Extensions.Configuration;

namespace FunOlympicDataManager.Library.DataAccess.Internal;

public interface ISqlDataAccess
{
    IConfiguration Configuration { get; }
    string GetConnectionString(string name);
    List<T> LoadData<T, U>(string storeProcedure, U parameters, string connectionStringName);
    List<T> LoadDataq<T, U>(string commandText, U parameters, string connectionStringName);
    T LoadFirstData<T, U>(string commandText, U parameters, string connectionStringName);
    int SaveData<T>(string storeProcedure, T parameters, string connectionStringName);
    int SaveDataQ<T>(string commantText, T parameters, string connectionStringName);
}