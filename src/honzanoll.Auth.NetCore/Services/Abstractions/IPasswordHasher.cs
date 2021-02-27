namespace honzanoll.Auth.NetCore.Services.Abstractions
{
    public interface IPasswordHasher
    {
        #region Public methods

        string Hash(string password);

        bool Verify(string password, string hash);

        #endregion
    }
}
