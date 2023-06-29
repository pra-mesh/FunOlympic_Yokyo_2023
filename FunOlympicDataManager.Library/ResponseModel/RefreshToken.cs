using FunOlympicDataManager.Library.ResponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FunOlympicDataManager.Library.Model;
public class RefreshTokenModel
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    public string RevokedByIp { get; set; }
    public string ReplacedByToken { get; set; }
    public string ReasonRevoked { get; set; }
    public string UserID { get; set; }
    public string UserName { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsRevoked && !IsExpired;
}
public class RefreshTokenResponse : BaseResponse 
{
    public RefreshTokenModel data { get; set; }
}
public class RefreshTokenListResponse : BaseResponse
{
    public List<RefreshTokenModel> data { get; set; }
}