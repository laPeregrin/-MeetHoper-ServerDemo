using _MeetHoper_ServerDemo.Services;
using Common.Models.Requests;
using Common.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {

        private readonly UserCredentialsService _userService;

        public AuthenticateController(UserCredentialsService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetPairTokens")]
        [ProducesResponseType(typeof(Response<PairTokenResponse>), 200)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationUserTokenRequset userRequest)
        {
            try
            {
                return Ok(await _userService.AuthenticateAsync(userRequest));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("SignUp")]
        [ProducesResponseType(typeof(Response<UserResponse>), 200)]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AuthenticationUserTokenRequset userRequest)
        {
            try
            {
                return Ok(await _userService.CreateAccount(userRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
