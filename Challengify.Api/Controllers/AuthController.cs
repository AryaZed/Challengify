using Challengify.Api.Models;
using Challengify.Api.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Challengify.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly GoogleAuthService _googleAuthService;

        public AuthController(SignInManager<User> signInManager, GoogleAuthService googleAuthService)
        {
            _signInManager = signInManager;
            _googleAuthService = googleAuthService;
        }

        [HttpGet("login")]
        public IActionResult LoginWithGoogle()
        {
            var redirectUrl = Url.Action(nameof(HandleGoogleResponse), "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> HandleGoogleResponse()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return BadRequest("Error retrieving login information.");

            var token = await _googleAuthService.HandleGoogleLogin(info);
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
    }
}
