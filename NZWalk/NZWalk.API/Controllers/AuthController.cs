using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Repositories.TokenRepository;
using NZWalk.API.Repositories.UserRepository;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("{authenticate}")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;
        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }


        [HttpPost]
        [Route("{login}")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                if(!ValidateLoginRequst(request))
                {
                    return BadRequest(ModelState);
                }

                var authenticUser = await userRepository.AuthenticateUserAsync(request.UserName, request.Password);
                if(authenticUser != null)
                {
                    //Generate JWT Token
                    var token = await tokenHandler.CreateTokenAsync(authenticUser);
                    return Ok(token);
                }

                return BadRequest("Incorrect UserName or Password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "server down");
            }
        }


        private bool ValidateLoginRequst(LoginRequest request)
        {
            if(String.IsNullOrWhiteSpace(request.UserName))
            {
                ModelState.AddModelError(nameof(request.UserName), "Incorrect username format");
            }

            if (String.IsNullOrWhiteSpace(request.Password))
            {
                ModelState.AddModelError(nameof(request.Password), "Password can not be empty or white space");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
    }
}
