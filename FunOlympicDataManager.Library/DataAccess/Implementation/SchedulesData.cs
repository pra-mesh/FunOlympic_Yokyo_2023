using FunOlympicDataManager.Library.DataAccess.Internal;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class SchedulesData
{
    private readonly ISqlDataAccess _sql;

    public SchedulesData(ISqlDataAccess sql)
    {
        _sql = sql;
    }


}
