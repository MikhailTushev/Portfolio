using System.Threading.Tasks;

namespace Portfolio.Common
{
    public interface INotificator
    {
        Task AddUserAsync(long userId, string connectionId);

        Task RemoveUserAsync(string connectionId);
    }
}