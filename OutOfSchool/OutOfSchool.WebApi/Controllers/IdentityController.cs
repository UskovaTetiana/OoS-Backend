using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OutOfSchool.WebApi.Models;
using OutOfSchool.WebApi.Services;
using OutOfSchool.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OutOfSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IJWTokenGenerator _tokenGenerator;
		private readonly RoleManager<IdentityUser> _roleManager;
		private readonly IConfiguration _config;
		private readonly IEmailService _emailService;

		public IdentityController (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJWTokenGenerator tokenGenerator,
			RoleManager<IdentityUser> roleManager, IConfiguration config, IEmailService emailService )
        {
			_emailService = emailService;
			_config = config;
			_roleManager = roleManager;
			_tokenGenerator = tokenGenerator;
			_signInManager = signInManager;
			_userManager = userManager;
        }

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var userFromDb = await _userManager.FindByEmailAsync(model.Email);
			
			if (userFromDb == null)
            {
				return BadRequest("Wrong Email");
            }

			var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, model.Password, false);

			if (!result.Succeeded)
            {
				return BadRequest("Wrong Password");
            }

			var roles = await _userManager.GetRolesAsync(userFromDb);

			IList<Claim> claims = await _userManager.GetClaimsAsync(userFromDb);
			return Ok(new
			{
				result = result,
				userEmail = model.Email,
				token = _tokenGenerator.GenerateToken(userFromDb, roles, claims)
			});
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			var userToCreate = new IdentityUser
			{
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(userToCreate, model.Password);

			if(result.Succeeded)
            {
				var userFromDb = await _userManager.FindByEmailAsync(model.Email);

				var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

				var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
				var query = HttpUtility.ParseQueryString(uriBuilder.Query);
				query["token"] = token;
				query["userid"] = userFromDb.Id;
				uriBuilder.Query = query.ToString();
				var urlString = uriBuilder.ToString();

				var senderEmail = _config["ReturnPaths:SenderEmail"];

				await _emailService.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);

				await _userManager.AddToRoleAsync(userFromDb, model.Role);




				return Ok(result);
            }
			return BadRequest(result);
		}

		[HttpPost("confirmemail")]
		public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
		{

			var user = await _userManager.FindByIdAsync(model.UserId);

			var result = await _userManager.ConfirmEmailAsync(user, model.Token);

			if (result.Succeeded)
			{
				return Ok();
			}
			return BadRequest();
		}
	}
}
