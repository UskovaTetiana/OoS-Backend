using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OutOfSchool.WebApi.Services
{
	public class JWTokenGenerator : IJWTokenGenerator
	{
		private readonly IConfiguration _config;

		public JWTokenGenerator(IConfiguration config)
		{
			_config = config;

		}
		public string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims)
		{
			// var claims = new List<Claim>
			// {
			// 	new Claim(JwtRegisteredClaimNames.GivenName , user.UserName),
			// 	new Claim(JwtRegisteredClaimNames.Email , user.Email),
			// };

			//claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.UserName));
			claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMonths(3),
				SigningCredentials = creds,
				Issuer = _config["Token:Issuer"],
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
