using JustChat.ViewModels;
using System.Threading.Tasks;

namespace JustChat.Repositories.Inrefaces
{
    public interface IAuthenticationRepository
    {
        Task<bool> Login(UserVM userVM);
        Task<bool> Register(UserVM userVM);
    }
}