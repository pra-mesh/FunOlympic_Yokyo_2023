using FunOlympicDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.ResponseModel;
public class ScoresResponse: BaseResponse
{
    public ScoreModel? data { get; set; }
}

public class ScoresListResponse : BaseResponse
{
    public List<ScoreModel>? data { get; set; }
}
