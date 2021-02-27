using Microsoft.Extensions.Options;
using honzanoll.Auth.Models.Settings;
using honzanoll.Auth.NetCore.Services.Abstractions;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace honzanoll.Auth.NetCore.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        #region Fields

        private readonly AuthenticationSettings authenticationSettings;

        #endregion

        #region Constructors

        public PasswordHasher(IOptions<AuthenticationSettings> authenticationSettingsOptions)
        {
            authenticationSettings = authenticationSettingsOptions.Value;
        }

        #endregion

        #region Public methods

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                  password,
                  Convert.FromBase64String(authenticationSettings.Salt),
                  authenticationSettings.IterationCount,
                  HashAlgorithmName.SHA512))
            {
                return Convert.ToBase64String(algorithm.GetBytes(authenticationSettings.KeySize));
            }
        }

        public bool Verify(string password, string hash)
        {
            var salt = Convert.FromBase64String(authenticationSettings.Salt);
            var key = Convert.FromBase64String(hash);

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              authenticationSettings.IterationCount,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(authenticationSettings.KeySize);

                return keyToCheck.SequenceEqual(key);
            }
        }

        #endregion
    }
}
