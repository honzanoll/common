using honzanoll.Data.Abstractions;
using System.Threading.Tasks;

namespace honzanoll.Auth.NetCore.Services.Abstractions
{
    public interface IUserService<TUser>
        where TUser : IEntity
    {
        #region Public methods

        Task<string> AuthenticateAsync(string email, string password);

        #endregion
    }
}
