using Education.API.Extensions;
using Education.API.Request;
using Education.Core.Responses;
using Education.Core.Security;
using Education.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Education.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Accesstoken(LoginRequest loginReqest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                _BaseResponse<AccessToken> accessTokenResponse = await authenticationService.CreateAccessToken(loginReqest.Email, loginReqest.Password);

                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.Extra);
                }
                else
                {
                    return BadRequest(accessTokenResponse.ErrorMessage);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenRequest tokenRequest)
        {
            _BaseResponse<AccessToken> accessTokenResponse = await authenticationService.CreateAccessTokenByRefreshToken(tokenRequest.RefreshToken);

            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(TokenRequest tokenRequest)
        {
            _BaseResponse<AccessToken> accessTokenResponse = await authenticationService.RevokeRefreshToken(tokenRequest.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }
    }
}
