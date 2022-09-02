using System.Threading.Tasks;

namespace Portfolio.Common
{
    public interface INotificationService
    {
        Task SendCreateMaskAsync(long userId);
    }
}