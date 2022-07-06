using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.Entities.Exceptions;
using PasswordManager.Entities.Shared;
using SecretsProvider;
using Solvintech.Contracts;
using Solvintech.Entities;
using Solvintech.Services.Contracts;

namespace Solvintech.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly JwtConfiguration _jwtConfiguration;
        private User _user; //todo delete mb

        public AuthenticationService(IRepositoryManager repository, IMapper mapper, IConfiguration configuration,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _jwtConfiguration = new JwtConfiguration();
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public async Task RegisterUser(UserForRegistrationDto userDto)
        {
            var user = new User()
            {
                Email = userDto.Email,
                Token = GenerateToken(userDto.Email),
                PasswordHash = ComputeSha256Hash(userDto.Password)
            };
            try
            {
                //try catch to send my exception except internal
                await _repository.Users.CreateUserAsync(user);
            }
            catch (SqlException)
            {
                throw new RegistrationNotSuccess("User with same email already exists");
            }
            catch (DbUpdateException)
            {
                throw new RegistrationNotSuccess("User with same email already exists");
            }
        }


        public async Task<string> GenerateNewUserToken(UserForAuthenticationDto userDto)
        {
            var newToken = GenerateToken(userDto.Email);
            await _repository.Users.UpdateUserTokenAsync(userDto.Email, ComputeSha256Hash(userDto.Password),
                newToken);
            return newToken;
        }

        public async Task<string> GetUserTokenAsync(UserForAuthenticationDto userDto) =>
            await GetUserTokenAsync(userDto.Email, userDto.Password);

        public async Task<string> GetUserTokenAsync(string email, string password) =>
            await _repository.Users.GetUserTokenAsync(email, ComputeSha256Hash(password));

        private string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        private string GenerateToken(string email)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(email);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return accessToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(JwtSecretsProvider.GetJwtSecretKey() ??
                                             throw new InvalidOperationException("secret key is null"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private IEnumerable<Claim> GetClaims(string email) =>
            new Claim[]
            {
                new(ClaimTypes.Email, email)
            };

        //creates an object of the JwtSecurityToken
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                _jwtConfiguration.ValidIssuer,
                _jwtConfiguration.ValidAudience,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}