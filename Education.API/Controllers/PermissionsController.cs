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
    [ApiController]
    public class PermissionsController : Controller
    {
        private readonly IPermissionService permissionService;
        private readonly IMapper mapper;
        public PermissionsController(IPermissionService permissionService, IMapper mapper)
        {
            this.permissionService = permissionService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FullList()
        {
            Guid permission = new Guid("5EAEC007-03D2-4DB0-9E4A-35AB20A0BE56");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            PermissionListResponse permissionListResponse = await permissionService.GetAllAsync();
            if (permissionListResponse.Success) return Ok(permissionListResponse); else return BadRequest(permissionListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindFullList([FromBody]PermissionDto permissionDto)
        {
            Guid permission = new Guid("C6485BA6-1005-46E9-8E11-FE822704A80F");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            PermissionListResponse permissionListResponse = await permissionService.FindAsync(permissionDto, true);
            if (permissionListResponse.Success) return Ok(permissionListResponse); else return BadRequest(permissionListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Guid permission = new Guid("68472158-A359-4FE7-8FFC-B9BC23041858");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            PermissionListResponse permissionListResponse = await permissionService.FindAsync(false);
            if (permissionListResponse.Success) return Ok(permissionListResponse); else return BadRequest(permissionListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find([FromBody]PermissionDto permissionDto)
        {
            Guid permission = new Guid("C72F0AF4-D3FD-4287-B3B1-FE540129A2C3");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            PermissionListResponse permissionListResponse = await permissionService.FindAsync(permissionDto, false);
            if (permissionListResponse.Success) return Ok(permissionListResponse); else return BadRequest(permissionListResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PermissionDto permissionDto)
        {
            Guid permission = new Guid("61AB7E33-0AE9-40B3-884E-81A140885941");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            PermissionResponse permissionResponse = await permissionService.AddAsync(permissionDto, userId);
            if (permissionResponse.Success) return Created("", permissionResponse); else return BadRequest(permissionResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPut]
        public IActionResult Update([FromBody]PermissionDto permissionDto)
        {
            Guid permissionId = new Guid("6904FA03-7DBB-4939-A2EB-0FF25033F1ED");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permissionId).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (permissionDto.Id == null || permissionDto.Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var permission = permissionService.GetByIdAsync(permissionDto.Id).Result;
            if (permission == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }

            PermissionResponse permissionResponse = permissionService.Update(permissionDto, userId);
            if (permissionResponse.Success) return Ok(permissionResponse); else return BadRequest(permissionResponse);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            Guid permission = new Guid("1E184DEF-F2AC-4D57-A32E-2F2D475A50F8");
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
            var entity = await permissionService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            PermissionResponse permissionResponse = await permissionService.Update(Id, userId);
            if (permissionResponse.Success) return Ok("Başarıyla silindi."); else return BadRequest(permissionResponse.ErrorMessage);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> FullDelete(Guid Id)
        {
            Guid permission = new Guid("F7EEDA55-75DF-4B9B-8C9D-E505AE51A199");
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
            var entity = await permissionService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            permissionService.Remove(entity);
            var check = await permissionService.GetByIdAsync(Id);
            if (check != null) return BadRequest("Silinme işlemi sırasında bir hata ile karşılaşıldı.");
            else return Ok("Başarıyla silindi.");
        }
    }
}

