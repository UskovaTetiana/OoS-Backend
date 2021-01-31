using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutOfSchool.WebApi.Services
{
	public interface IJWTokenGenerator
	{
		string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims);
	}
}
