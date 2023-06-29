using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.ResponseModel;
public class BooleanResponse:BaseResponse
{
    public bool data { get; set; } = false;
}
