using FooCargo.Authorization;
using FooCargo.CoreModels;
using FooCargo.Models;
using FooCargo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWT jwt;

        // We are calling the base contructor that requires instance of CargoDb. This instance supplied by Dependency Injection and passed to parent class (BaseController) through base(context). The base controller is storing this instance in the readonly property Db that can be used in all the inherting controllers.
        public AccountController(CargoDb context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, JWT jwt) : base(context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.jwt = jwt;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<UserProfile> Profile()
        {
            // this.User is filled asp.net after validating JWT
            ApplicationUser user = await userManager.FindByNameAsync(this.User.Identity.Name);
            return user.ToUserProfile();
            
            // the above called a c# extension method which is a shortcut to the line below
            //return ModelExtensions.ToUserProfile(user);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResult>> Login(LoginInfo loginInfo)
        {
            if (loginInfo?.Email == null || loginInfo?.Password == null)
            {
                return Unauthorized();
            }

            var user = await userManager.FindByEmailAsync(loginInfo.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var res = await signInManager.CheckPasswordSignInAsync(user, loginInfo.Password, true);
            if (res.Succeeded)
            {
                var token = await jwt.GenerateJwtTokenAsync(user);
                var loginResult = new LoginResult { Token = token };
                return Ok(loginResult);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterInfo registerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser { UserName = registerInfo.Email, Email = registerInfo.Email, FullName = registerInfo.FullName };
            var res = await userManager.CreateAsync(user, registerInfo.Password);
            if (res.Succeeded)
            {
                // this code is just for practicing how to add a policy, in real life, registration should add authorization policies
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(Claims.ADMIN, "true"));

                return NoContent();
            }
            else return BadRequest(res.Errors);
        }

        //Code On How To Update User

        async Task CodeOnHowToUpdateUser()
        {
            var user = await userManager.FindByEmailAsync("hamdanfrias@gmail.com");
            user.EmailConfirmed = true;
            user.FullName = "Firas";
            await userManager.UpdateAsync(user);
        }
    }
}
