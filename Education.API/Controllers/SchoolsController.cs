using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Education.API.Filters;
using Education.Core.DTOs;
using Education.Core.Models;
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
    public class SchoolsController : Controller
    {
        private readonly ISchoolService schoolService;
        private readonly IMapper mapper;
        public SchoolsController(ISchoolService schoolService, IMapper mapper)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FullList()
        {
            Guid permission = new Guid("A496182E-2B49-4B21-A20C-C378CE6AB5F9");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            SchoolListResponse schoolListResponse = await schoolService.GetAllAsync();
            if (schoolListResponse.Success) return Ok(schoolListResponse); else return BadRequest(schoolListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindFullList([FromBody]SchoolDto schoolDto)
        {
            Guid permission = new Guid("7A11C68A-0CCF-4350-82B7-291A37CF5D26");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            SchoolListResponse schoolListResponse = await schoolService.FindAsync(schoolDto, true);
            if (schoolListResponse.Success) return Ok(schoolListResponse); else return BadRequest(schoolListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Guid permission = new Guid("85DB52CE-AE1B-40F9-B811-4FF34AB16A58");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            SchoolListResponse schoolListResponse = await schoolService.FindAsync(false);
            if (schoolListResponse.Success) return Ok(schoolListResponse); else return BadRequest(schoolListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find([FromBody]SchoolDto schoolDto)
        {
            Guid permission = new Guid("CB6C5FBD-2244-4791-BA27-BAB3970D01ED");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            SchoolListResponse schoolListResponse = await schoolService.FindAsync(schoolDto, false);
            if (schoolListResponse.Success) return Ok(schoolListResponse); else return BadRequest(schoolListResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]SchoolDto schoolDto)
        {
            Guid permission = new Guid("DF5B1707-C390-4088-B76C-6CCA122EFCEB");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            SchoolResponse schoolResponse = await schoolService.AddAsync(schoolDto, userId);
            if (schoolResponse.Success) return Created("", schoolResponse); else return BadRequest(schoolResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPut]
        public IActionResult Update([FromBody]SchoolDto schoolDto)
        {
            Guid permission = new Guid("9C941F19-4AF0-4B35-8160-74B23858A38F");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (schoolDto.Id == null || schoolDto.Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var school = schoolService.GetByIdAsync(schoolDto.Id).Result;
            if (school == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }

            SchoolResponse schoolResponse = schoolService.Update(schoolDto, userId);
            if (schoolResponse.Success) return Ok(schoolResponse); else return BadRequest(schoolResponse);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            Guid permission = new Guid("EDDA3D4D-CEF0-474C-9A3B-5510675D1C19");
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
            var entity = await schoolService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            SchoolResponse schoolResponse = await schoolService.Update(Id, userId);
            if (schoolResponse.Success) return Ok("Başarıyla silindi."); else return BadRequest(schoolResponse.ErrorMessage);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> FullDelete(Guid Id)
        {
            Guid permission = new Guid("C7FCB62D-E475-404F-9A46-40D17C6B8FE3");
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
            var entity = await schoolService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            schoolService.Remove(entity);
            var check = await schoolService.GetByIdAsync(Id);
            if (check != null) return BadRequest("Silinme işlemi sırasında bir hata ile karşılaşıldı.");
            else return Ok("Başarıyla silindi.");
        }
    }
}
