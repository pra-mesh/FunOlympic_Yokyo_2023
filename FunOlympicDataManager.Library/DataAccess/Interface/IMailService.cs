using FunOlympicDataManager.Library.Models;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IMailService
{
    Task<bool> SendAsync(MailData mailData, CancellationToken ct = default);
}