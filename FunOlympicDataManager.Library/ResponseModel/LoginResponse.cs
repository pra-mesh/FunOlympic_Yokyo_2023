using FunOlympicDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.ResponseModel;
public class LoginResponse : BaseResponse
{
    public TokenModel ? data { get; set; }
}