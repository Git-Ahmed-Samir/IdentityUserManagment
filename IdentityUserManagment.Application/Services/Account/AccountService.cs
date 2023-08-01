using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;
using IdentityUserManagment.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityUserManagment.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountService(IConfiguration configuration, UserManager<User> userManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseModel<AuthModel>> RegisterAsync(RegisterDto model)
        {
            if (model == null)
                return Response<AuthModel>.Failed("Invalid Data", (int)HttpStatusCode.BadRequest);

            //check if user already exists
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return Response<AuthModel>.Failed("Emaily already Exist", (int)HttpStatusCode.BadRequest);
            
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return Response<AuthModel>.Failed("Username already Exist", (int)HttpStatusCode.BadRequest);

            var user = _mapper.Map<User>(model);
            user.EmailConfirmed = true; // set email confirmed to true

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return Response<AuthModel>.Failed(string.Join("\n", result.Errors.Select(x => x.Description)), (int)HttpStatusCode.BadRequest);
            

            // Add Roles To Users
            var addRole = await _userManager.AddToRoleAsync(user, Roles.User);
            if (!addRole.Succeeded)
                return Response<AuthModel>.Failed(string.Join("\n", addRole.Errors.Select(x => x.Description)), (int)HttpStatusCode.BadRequest);

            var authModel = await GetAuthData(user);

            return Response<AuthModel>.Success(authModel);
        }

        public async Task<ResponseModel<AuthModel>> LoginAsync(LoginDto model)
        {
            if (model == null)
                return Response<AuthModel>.Failed("Invalid data", (int)HttpStatusCode.BadRequest);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Response<AuthModel>.Failed("Email or password are wrong", (int)HttpStatusCode.BadRequest);

            var isPasswordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!isPasswordMatch)
                return Response<AuthModel>.Failed("Email or password are wrong", (int)HttpStatusCode.BadRequest);

            var authModel = await GetAuthData(user);

            return Response<AuthModel>.Success(authModel);
        }

        private async Task<AuthModel> GetAuthData(User user)
        {
            var jwtSecurityToken = await GenerateToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);
            var authModel = new AuthModel
            {
                Email = user.Email,
                Id = user.Id,
                Username = user.UserName,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Roles = rolesList.ToList()
            };
            return authModel;
        }
        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var durationInDays = int.Parse(_configuration["Jwt:DurationInDays"]);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddDays(durationInDays)
            );

            return jwtSecurityToken;
        }
    }
}