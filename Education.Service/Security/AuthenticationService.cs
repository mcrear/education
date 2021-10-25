using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using Education.Core.Security;
using Education.Core.Services;
using Education.Data.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Service.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IPermissionService permissionService;

        private readonly ITokenHandler tokenHandler;

        private readonly IMapper mapper;

        public AuthenticationService(IUserService userService, IPermissionService permissionService, ITokenHandler tokenHandler, IMapper mapper)
        {
            this.userService = userService;
            this.permissionService = permissionService;
            this.tokenHandler = tokenHandler;
            this.mapper = mapper;

        }

        public async Task<_BaseResponse<AccessToken>> CreateAccessToken(string email, string password)
        {
            var userResponse = userService.FindByEmailAndPassword(email, password).Result;
            if (userResponse.Success)
            {
                var permissionResponse = await permissionService.GetPermissionsByUserId(userResponse.Extra.Id);
                List<Guid> permissionList = new List<Guid>();
                if (permissionResponse.Success)
                    foreach (PermissionDto item in permissionResponse.Extra)
                    {
                        permissionList.Add(item.Id);
                    }
                AccessToken accessToken = tokenHandler.CreateAccessToken(mapper.Map<UserDto, User>(userResponse.Extra), permissionList);

                await userService.SaveRefreshToken(userResponse.Extra.Id, accessToken.RefreshToken, 5);

                return new _BaseResponse<AccessToken>(accessToken);
            }
            else
            {
                return new _BaseResponse<AccessToken>(userResponse.ErrorMessage);
            }
        }

        public async Task<_BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse = await userService.GetUserByRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                var permissionResponse = await permissionService.GetPermissionsByUserId(userResponse.Extra.Id);
                List<Guid> permissionList = new List<Guid>();
                if (permissionResponse.Success)
                    foreach (PermissionDto item in permissionResponse.Extra)
                    {
                        permissionList.Add(item.Id);
                    }
                if (userResponse.Extra.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(mapper.Map<UserDto, User>(userResponse.Extra), permissionList);

                    await userService.SaveRefreshToken(userResponse.Extra.Id, accessToken.RefreshToken, 1);

                    return new _BaseResponse<AccessToken>(accessToken);
                }
                else
                {
                    return new _BaseResponse<AccessToken>("refreshtoken suresi dolmus");
                }
            }
            else
            {
                return new _BaseResponse<AccessToken>("refreshtoken bulunamadı");
            }
        }

        public async Task<_BaseResponse<AccessToken>> RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse = await userService.GetUserByRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                await userService.RemoveRefreshToken(mapper.Map<UserDto, User>(userResponse.Extra));

                return new _BaseResponse<AccessToken>(new AccessToken());
            }
            else
            {
                return new _BaseResponse<AccessToken>("refreshtoken bulunamadı.");
            }
        }
    }
}