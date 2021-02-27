namespace honzanoll.Auth.Models.Settings
{
    public class AuthenticationSettings
    {
        #region Properties

        public string Salt { get; set; }

        public int IterationCount { get; set; } = 10000;

        public int KeySize { get; set; } = 32;

        public AuthenticationJwtSettings Jwt { get; set; }

        #endregion

        public class AuthenticationJwtSettings
        {
            #region Properties

            public int ExpirationTime { get; set; } = 12;

            public string SecretKey { get; set; }

            public string Issuer { get; set; }

            public string Audience { get; set; }

            #endregion
        }
    }
}
