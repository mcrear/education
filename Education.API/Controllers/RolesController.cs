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
    public class RolesController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FullList()
        {
            Guid permission = new Guid("1B37575C-208C-4B4D-95B1-07FFA6F235E4");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            RoleListResponse roleListResponse = await roleService.GetAllAsync();
            if (roleListResponse.Success) return Ok(roleListResponse); else return BadRequest(roleListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindFullList([FromBody]RoleDto roleDto)
        {
            Guid permission = new Guid("44C92D7B-16A9-48B9-B307-559B429F4699");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            RoleListResponse roleListResponse = await roleService.FindAsync(roleDto, true);
            if (roleListResponse.Success) return Ok(roleListResponse); else return BadRequest(roleListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Guid permission = new Guid("B7AF76D4-A0C1-4D16-9FF7-4CB65841469E");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            RoleListResponse roleListResponse = await roleService.FindAsync(false);
            if (roleListResponse.Success) return Ok(roleListResponse); else return BadRequest(roleListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find([FromBody]RoleDto roleDto)
        {
            Guid permission = new Guid("2F32C7DF-7EE4-4463-9ADD-14DDACBA9570");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            RoleListResponse roleListResponse = await roleService.FindAsync(roleDto, false);
            if (roleListResponse.Success) return Ok(roleListResponse); else return BadRequest(roleListResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]RoleDto roleDto)
        {
            Guid permission = new Guid("317F500E-9F8B-446A-A18E-6BB051FC19B7");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            RoleResponse roleResponse = await roleService.AddAsync(roleDto, userId);
            if (roleResponse.Success) return Created("", roleResponse); else return BadRequest(roleResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]RoleDto roleDto)
        {
            Guid permission = new Guid("9ED0F042-4A6C-4B01-9B26-5C3D334F672B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (roleDto.Id == null || roleDto.Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var role = await roleService.GetByIdAsync(roleDto.Id);
            if (role == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }

            RoleResponse roleResponse = await roleService.Update(roleDto, userId);
            if (roleResponse.Success) return Ok(roleResponse); else return BadRequest(roleResponse);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            Guid guidId = Id;// new Guid(Id);

            Guid permission = new Guid("B560C1F4-1776-43BE-AAE5-7588FAEB234B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (guidId == null || guidId == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var entity = await roleService.GetByIdAsync(guidId);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            RoleResponse roleResponse = await roleService.Update(guidId, userId);
            if (roleResponse.Success) return Ok("Başarıyla silindi."); else return BadRequest(roleResponse.ErrorMessage);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> FullDelete(Guid Id)
        {
            Guid permission = new Guid("4D0AE9A1-3CDE-4094-816B-FDF5F54049EF");
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
            var entity = await roleService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            roleService.Remove(entity);
            var check = roleService.GetByIdAsync(Id).Result;
            if (check != null) return BadRequest("Silinme işlemi sırasında bir hata ile karşılaşıldı.");
            else return Ok("Başarıyla silindi.");
        }
    }
}
