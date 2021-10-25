using Education.Core.Responses;
using System.Threading.Tasks;

namespace Education.Core.Security
{
    public interface IAuthenticationService
    {
        Task<_BaseResponse<AccessToken>> CreateAccessToken(string email, string password);

        Task<_BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(string refreshToken);

        Task<_BaseResponse<AccessToken>> RevokeRefreshToken(string refreshToken);
    }
}
