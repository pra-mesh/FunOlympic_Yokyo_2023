using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.ResponseModel;
public class BadRequestResponse : BaseResponse
{
    public List<string>? data { get; set; }
}
