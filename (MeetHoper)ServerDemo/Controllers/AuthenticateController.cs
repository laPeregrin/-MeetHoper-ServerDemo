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
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {

        private readonly UserCredentialsService _userService;

        public AuthenticateController(UserCredentialsService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetPairTokens")]
        [ProducesResponseType(typeof(Response<PairTokenResponse>), 200)]
        public async Task<IActionResult> GetPairTokensAsync([FromBody] AuthenticationUserTokenRequset userRequest)
        {
            try
            {
                return Ok(await _userService.GetPairTokensAsync(userRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(Response<PairTokenResponse>), 200)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest userRequest)
        {
            try
            {
                return Ok(await _userService.LoginAsync(userRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("CreateAccount")]
        [ProducesResponseType(typeof(Response<UserTokenResponse>), 200)]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest userRequest)
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

        [Authorize]
        [HttpPost("UpdateAccount")]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] UpdateUserDataRequest userRequest)
        {
            try
            {
                return Ok(await _userService.UpdateAccount(userRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("HearBeatAuth")]
        public IActionResult HearBeatAuth() => Ok("Succes!");

    }
}
