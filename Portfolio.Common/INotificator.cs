using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Common
{
    public interface INotificator
    {
        Task SendCreateMaskAsync(long userId);
    }
}