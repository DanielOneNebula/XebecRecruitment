using AutoMapper;
using XebecPortal.Shared.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Server.IRepository;
using Microsoft.AspNetCore.Authentication;


namespace Server.Controllers
{
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserDb userDb;

		public AuthController(IUserDb userDb)
		{
			this.userDb = userDb;
		}
		 
		private string CreateJWT(AppUser user)
		{
			var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS IS THE SECRET KEY"));
			// NOTE: SAME KEY AS USED IN Startup.cs FILE

			var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

			var claims = new[] // NOTE: could also use List<Claim> here
			{
				new Claim(ClaimTypes.Name, user.Email), // NOTE: this will be the "User.Identity.Name" value
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, user.Email) 
				// NOTE: this could a unique ID assigned to the user by a database
			};

			var token = new JwtSecurityToken(issuer: "domain.com", audience: "domain.com", claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}



		[HttpPost]
		[Route("api/auth/register")]
		public async Task<LoginResult> Post([FromBody] RegisterModel reg)
		{
			if (reg.Password != reg.ConfirmPassword)
				return new LoginResult { Message = "Password and confirm password do not match.", Success = false };

			AppUser newuser = await userDb.AddUser(reg.Email, reg.Password, reg.Role);

			if (newuser != null)

				return new LoginResult
				{
					Message = "Registration successful.",
					JwtBearer = CreateJWT(newuser),
					Email = reg.Email,
					Role = reg.Role,
					Success = true,
					AppUserId = newuser.Id
				};

			return new LoginResult { Message = "User already exists.", Success = false };

		}

		[HttpPost]
		[Route("api/auth/login")]
		public async Task<LoginResult> Post([FromBody] LoginModel log)
		{
			AppUser user = await userDb.AuthenticateUser(log.Email, log.Password);

			if (user != null)
				return new LoginResult
				{
					Id = user.Id,//<-newly added
					Message = "Login successful.",
					JwtBearer = CreateJWT(user),
					Email = log.Email,
					Role = user.Role,
					Success = true
				};

			return new LoginResult { Message = "User/password not found.", Success = false };

		}


		

	}
}