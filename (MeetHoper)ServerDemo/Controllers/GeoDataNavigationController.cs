using Common.Abstractions;
using Common.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GeoDataNavigationController : ControllerBase
    {
        private readonly IUserGeoNavigationService _userGeoNavigationService;

        public GeoDataNavigationController(IUserGeoNavigationService userGeoService)
        {
            _userGeoNavigationService = userGeoService;
        }

        [HttpGet("GetUsersNear")]
        [ProducesResponseType(typeof(Response<UserCollectionResponse>), 200)]
        public async Task<IActionResult> GetUserNearAsync([FromQuery(Name = "UserId")] Guid id,
                                                          [FromQuery] double longitude,
                                                          [FromQuery] double latitude)
        {
            try
            {
               return Ok(await _userGeoNavigationService.GetNearResponseAsync(id, new Common.Models.DTOs.Geoposition(longitude, latitude)));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
