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
                Username = userDto.Username,
                Email = userDto.Email,
                Token = GenerateToken(userDto.Username, userDto.Email),
                PasswordHash = ComputeSha256Hash(userDto.Password)
            };
            try
            {
                await _repository.Users.CreateUser(user);
            }
            catch (SqlException)
            {
                throw new RegistrationNotSuccess("User with same username already exists");
            }
            catch (DbUpdateException)
            {
                throw new RegistrationNotSuccess("User with same username already exists");
            }
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            // _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            // var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
            // if (!result)
            //     _logger.LogWarn(
            //         $"{nameof(ValidateUser)}: Authentication failed. Wrong username or password. USER_ID={(_user?.Id != null ? _user.Id : "null")}");
            // return result;
            return true;
        }

        public async Task<string> GetToken()
        {
            throw new NotImplementedException();
        }

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


        private string GenerateToken(string userName, string email)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(userName, email);
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

        private IEnumerable<Claim> GetClaims(string userName, string email)
        {
            return new Claim[]
            {
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.Email, email)
            };
        }

        //creates an object of the JwtSecurityToken
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                _jwtConfiguration.ValidIssuer,
                _jwtConfiguration.ValidAudience,
                claims,
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}