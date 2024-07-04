using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIMastery.Models.Domain;
using WebAPIMastery.Repositories;

namespace WebAPIMastery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public RegisterController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(Register register)
        {
            var identityUser = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, register.Password);

            if(identityResult.Succeeded)
            {
                if(register.Roles != null && register.Roles.Any())
                {
                    identityResult = await userManager.AddToRoleAsync(identityUser, register.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("The User " + identityUser.UserName + " has been registered");
                    }
                }
            }

            return BadRequest("Something went wrong");
        
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var user = await userManager.FindByEmailAsync(loginUser.Username);

            if(user!= null)
            {
                var checkPasswordresult = await userManager.CheckPasswordAsync(user, loginUser.Password);

                if(checkPasswordresult)
                {
                    var role = await userManager.GetRolesAsync(user);

                    if(role != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, role.ToList());
                        return Ok(jwtToken);
                    }
                   
                }

            }

            return BadRequest();
        }


    }
}
