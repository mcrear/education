using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Education.API.Filters;
using Education.Core.DTOs;
using Education.Core.Responses;
using Education.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Education.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPermissionService permissionService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IRoleService roleService, IPermissionService permissionService, IMapper mapper)
        {
            this.roleService = roleService;
            this.permissionService = permissionService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            var user = await userService.GetResponseByIdAsync(userId);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(user.ErrorMessage);
            }
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetPermissions()
        //{
        //    IEnumerable<Claim> claims = User.Claims;

        //    Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
        //    var permissions = await permissionService.GetPermissionsByUserId(userId);
        //    if (permissions != null && permissions.Extra != null && permissions.Extra.Count() > 0)
        //    {
        //        return Ok(permissions);
        //    }
        //    else if (permissions != null && permissions.Extra != null && permissions.Extra.Count() == 0)
        //    {
        //        return Ok("Belirtilen kullanıcıya ait yetki bulunmamaktadır.");
        //    }
        //    else
        //        return BadRequest(permissions);
        //}

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            IEnumerable<Claim> claims = User.Claims;

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            var permissions = await roleService.GetRolesByUserId(userId);
            if (permissions != null && permissions.Extra != null && permissions.Extra.Count() > 0)
            {
                return Ok(permissions);
            }
            else if (permissions != null && permissions.Extra != null && permissions.Extra.Count() == 0)
            {
                return Ok("Belirtilen kullanıcıya ait rol bulunmamaktadır.");
            }
            else
                return BadRequest(permissions.ErrorMessage);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            IEnumerable<Claim> claims = User.Claims;

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FullList()
        {
            Guid permission = new Guid("D26CCDC1-B84D-4393-9C19-4482EBDF894B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            UserListResponse userListResponse = await userService.GetAllAsync();
            if (userListResponse.Success) return Ok(userListResponse); else return BadRequest(userListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindFullList([FromBody]UserDto userDto)
        {
            Guid permission = new Guid("25CC07DD-85D1-4CB7-BD78-F4D6685FF78A");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            UserListResponse userListResponse = await userService.FindAsync(userDto, true);
            if (userListResponse.Success) return Ok(userListResponse); else return BadRequest(userListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Guid permission = new Guid("0CDE5284-02DB-4650-BF80-2C2D2CFDA7ED");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            UserListResponse userListResponse = await userService.FindAsync(false);
            if (userListResponse.Success) return Ok(userListResponse); else return BadRequest(userListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find([FromBody]UserDto userDto)
        {
            Guid permission = new Guid("EAFCE107-ABB1-49D3-BBC9-1920FE2DC58B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            UserListResponse userListResponse = await userService.FindAsync(userDto, false);
            if (userListResponse.Success) return Ok(userListResponse); else return BadRequest(userListResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserDto userDto)
        {
            Guid permission = new Guid("FBB9170D-BDCE-48BE-981B-925E651A4D63");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            UserResponse userResponse = await userService.AddAsync(userDto, userId);
            if (userResponse.Success) return Created("", userResponse); else return BadRequest(userResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPut]
        public IActionResult Update([FromBody]UserDto userDto)
        {
            Guid permission = new Guid("9B5CD2F7-61A6-4563-B659-97EB67594170");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (userDto.Id == null || userDto.Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var user = userService.GetByIdAsync(userDto.Id).Result;
            if (user == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }

            UserResponse userResponse = userService.Update(userDto, userId);
            if (userResponse.Success) return Ok(userResponse); else return BadRequest(userResponse);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            Guid permission = new Guid("CA44E363-B0D9-4EFB-9FC7-D5EEA8BAFCA0");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (Id == null || Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var entity = await userService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            UserResponse userResponse = await userService.Update(Id, userId);
            if (userResponse.Success) return Ok("Başarıyla silindi."); else return BadRequest(userResponse.ErrorMessage);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> FullDelete(Guid Id)
        {
            Guid permission = new Guid("54B70B8A-C872-40EF-B0E7-F2809327A77A");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }


            if (Id == null || Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var entity = await userService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            userService.Remove(entity);
            var check = await userService.GetByIdAsync(Id);
            if (check != null) return BadRequest("Silinme işlemi sırasında bir hata ile karşılaşıldı.");
            else return Ok("Başarıyla silindi.");
        }
    }
}
