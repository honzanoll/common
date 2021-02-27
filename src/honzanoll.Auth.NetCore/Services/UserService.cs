using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using honzanoll.Auth.Models;
using honzanoll.Auth.Models.Settings;
using honzanoll.Auth.NetCore.Services.Abstractions;
using honzanoll.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace honzanoll.Auth.NetCore.Services
{
    public class UserService<TUser> : IUserService<TUser>
        where TUser : UserEntity
    {
        #region Fields

        public readonly IRepository<TUser> userRepository;

        private readonly IPasswordHasher passwordHasher;

        private readonly AuthenticationSettings authenticationSettings;

        #endregion

        #region Constructors

        public UserService(
            IRepository<TUser> userRepository,
            IPasswordHasher passwordHasher,
            IOptions<AuthenticationSettings> authenticationSettings)
        {
            this.userRepository = userRepository;

            this.passwordHasher = passwordHasher;

            this.authenticationSettings = authenticationSettings.Value;
        }

        #endregion

        #region Public methods

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            TUser user = await userRepository.GetAsync(u => u.Email == email);
            if (user == null)
                throw new UnauthorizedAccessException("Unknown user");

            if (!passwordHasher.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid password");

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Jwt.SecretKey));
            SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: authenticationSettings.Jwt.Issuer,
                audience: authenticationSettings.Jwt.Audience,
                claims: GetClaims(user),
                expires: DateTime.Now.AddHours(authenticationSettings.Jwt.ExpirationTime),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        #endregion

        #region Private methods

        private List<Claim> GetClaims(TUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            // TODO: map user roles claims
            //foreach (UserRole role in user.Roles.GetValuesByFlags())
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            //}

            return claims;
        }

        #endregion
    }
}
