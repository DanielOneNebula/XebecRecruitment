using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using XebecPortal.Shared.Security;
using Server.IRepository;
using Server.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirdPartyUserController : ControllerBase
    {
        private readonly IUserDb userDb;
        private readonly ApplicationDbContext context;

        public ThirdPartyUserController(IUserDb userDb, ApplicationDbContext context)
        {
            this.userDb = userDb;
            this.context = context;
        }


        //   [HttpGet("GoogleResponse")]
        // public async Task<IActionResult> GoogleResponse()
        // {
        //     var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //     var claims = result.Principal.Identities.FirstOrDefault()
        //     .Claims.Select(claim => new
        //     {

        //         claim.Issuer,
        //         claim.OriginalIssuer,
        //         claim.Type,
        //         claim.Value

        //     }).Where(q => q.Type == ClaimTypes.Email);

        //     var email = (claims == null ? string.Empty : claims.FirstOrDefault().Value);
        //     email = email == null ? string.Empty : email;

        //     return new JsonResult(email);

        // }

        [HttpGet("GithubResponse")]
        public async Task GithubResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {

                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value

            }).Where(q => q.Type == ClaimTypes.Email);

            var email = (claims == null ? string.Empty : claims.FirstOrDefault().Value);
            email = email == null ? string.Empty : email;


            if (!string.IsNullOrEmpty(email))
            {
                RegisterModel reg = new RegisterModel();
                reg.Password = "P@ssword1";
                reg.Role = "Candidate";
                reg.Email = email;
                var users = context.AppUser.FirstOrDefault(q => q.Email.Equals(email));

                if (users == null)
                {
                    AppUser newuser = await userDb.AddUser(reg.Email, reg.Password, reg.Role);
                }
                else
                {
                    email = "already in db";
                }

            }

            HttpContext.Response.Redirect("/profile_");

        }
        /*GitHub OAuth*/
        [HttpGet("GitHubSignIn")]
        public async Task<IActionResult> GitHubSignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("GithubResponse") }, "Github");
        }


        [HttpGet("GoogleResponse")]
        public async Task GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {

                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value

            }).Where(q => q.Type == ClaimTypes.Email);

            var email = (claims == null ? string.Empty : claims.FirstOrDefault().Value);
            email = email == null ? string.Empty : email;


            if (!string.IsNullOrEmpty(email))
            {
                RegisterModel reg = new RegisterModel();
                reg.Password = "P@ssword1";
                reg.Role = "Candidate";
                reg.Email = email;
                var users = context.AppUser.FirstOrDefault(q => q.Email.Equals(email));

                if (users == null)
                {
                    AppUser newuser = await userDb.AddUser(reg.Email, reg.Password, reg.Role);
                    
                }
                else
                {
                    email = "already in db";
                }

            }


            HttpContext.Response.Redirect("/profile_");

        }

        [HttpGet("GoogleSignIn")]
        public async Task<IActionResult> GoogleSignInAsync()
        {

            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") }, "Google");

        }


        [HttpGet("TwitterResponse")]
        public async Task TwitterResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {

                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value

            }).Where(q => q.Type == ClaimTypes.Email);

            var email = (claims == null ? string.Empty : claims.FirstOrDefault().Value);
            email = email == null ? string.Empty : email;


            if (!string.IsNullOrEmpty(email))
            {
                RegisterModel reg = new RegisterModel();
                reg.Password = "P@ssword1";
                reg.Role = "Candidate";
                reg.Email = email;
                var users = context.AppUser.FirstOrDefault(q => q.Email.Equals(email));

                if (users == null)
                {
                    AppUser newuser = await userDb.AddUser(reg.Email, reg.Password, reg.Role);
                }
                else
                {
                    email = "already in db";
                }
           }
            
            HttpContext.Response.Redirect("/profile_");

        }

        [HttpGet("TwitterSignIn")]
        public async Task<IActionResult> TwitterSignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("TwitterResponse") }, "Twitter");

        }


        [HttpGet("LinkedInResponse")]
        public async Task LinkedInResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {

                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value

            }).Where(q => q.Type == ClaimTypes.Email);

            var email = (claims == null ? string.Empty : claims.FirstOrDefault().Value);
            email = email == null ? string.Empty : email;


            if (!string.IsNullOrEmpty(email))
            {
                RegisterModel reg = new RegisterModel();
                reg.Password = "P@ssword1";
                reg.Role = "Candidate";
                reg.Email = email;
                var users = context.AppUser.FirstOrDefault(q => q.Email.Equals(email));

                if (users == null)
                {
                    AppUser newuser = await userDb.AddUser(reg.Email, reg.Password, reg.Role);
                }
                else
                {
                    email = "already in db";
                }

            }

            HttpContext.Response.Redirect("/profile_");

        }
        [HttpGet("LinkedInSignIn")]
        public async Task<IActionResult> LinkedInSignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("LinkedInResponse") }, "LinkedIn");

        }

        [HttpGet("LogOut")]
        public async Task LogOut()
        {
            {
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in siteCookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }

            await HttpContext.SignOutAsync();
            HttpContext.Response.Redirect("/");
            HttpContext.Session.Clear();


        }

    }


}
