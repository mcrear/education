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
    public class TopicsController : Controller
    {
        private readonly ITopicService topicService;
        private readonly IMapper mapper;
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            this.topicService = topicService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FullList()
        {
            Guid permission = new Guid("22FF0A60-AC9A-4210-8E39-908A5CC0E15C");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            TopicListResponse topicListResponse = await topicService.GetAllAsync();
            if (topicListResponse.Success) return Ok(topicListResponse); else return BadRequest(topicListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FindFullList([FromBody]TopicDto topicDto)
        {
            Guid permission = new Guid("7F180674-9504-40D0-AE99-CF033BB0D59B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            TopicListResponse topicListResponse = await topicService.FindAsync(topicDto, true);
            if (topicListResponse.Success) return Ok(topicListResponse); else return BadRequest(topicListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            Guid permission = new Guid("132B4132-3958-4006-91B6-9E218B1BC829");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            TopicListResponse topicListResponse = await topicService.FindAsync(false);
            if (topicListResponse.Success) return Ok(topicListResponse); else return BadRequest(topicListResponse);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find([FromBody]TopicDto topicDto)
        {
            Guid permission = new Guid("03294570-879B-4958-892D-EE9D5C96E337");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            TopicListResponse topicListResponse = await topicService.FindAsync(topicDto, false);
            if (topicListResponse.Success) return Ok(topicListResponse); else return BadRequest(topicListResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TopicDto topicDto)
        {
            Guid permission = new Guid("7EF7655E-4A50-4D39-A125-2DC200DE0E35");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            TopicResponse topicResponse = await topicService.AddAsync(topicDto, userId);
            if (topicResponse.Success) return Created("", topicResponse); else return BadRequest(topicResponse);
        }

        [Authorize]
        [ValidationFilter]
        [HttpPut]
        public IActionResult Update([FromBody]TopicDto topicDto)
        {
            Guid permission = new Guid("B034333B-41D2-4295-ADCF-9D475E3EBF0B");
            IEnumerable<Claim> claims = User.Claims;
            List<Guid> permissions = JsonConvert.DeserializeObject<List<Guid>>(claims.Where(c => c.Type == "Permissions").First().Value);
            if (permissions.Where(x => x == permission).Count() == 0)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Bu işlem için yetki gereklidir." });
                return Unauthorized(new ErrorListResponse(errors));
            }

            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);

            if (topicDto.Id == null || topicDto.Id == Guid.Empty)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Id alanı gereklidir." });
                return BadRequest(new ErrorListResponse(errors));
            }
            var topic = topicService.GetByIdAsync(topicDto.Id).Result;
            if (topic == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }

            TopicResponse topicResponse = topicService.Update(topicDto, userId);
            if (topicResponse.Success) return Ok(topicResponse); else return BadRequest(topicResponse);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            Guid permission = new Guid("3E11136D-5D96-42AA-9119-F9F1DDE3F8B1");
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
            var entity = await topicService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            TopicResponse topicResponse = await topicService.Update(Id, userId);
            if (topicResponse.Success) return Ok("Başarıyla silindi."); else return BadRequest(topicResponse.ErrorMessage);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> FullDelete(Guid Id)
        {
            Guid permission = new Guid("399961D3-4051-4EEE-B672-1126086B2F7C");
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
            var entity = await topicService.GetByIdAsync(Id);
            if (entity == null)
            {
                List<ErrorDto> errors = new List<ErrorDto>();
                errors.Add(new ErrorDto { Error = "Belirtilen kayıt bulunamadı." });
                return NotFound(new ErrorListResponse(errors));
            }


            topicService.Remove(entity);
            var check = await topicService.GetByIdAsync(Id);
            if (check != null) return BadRequest("Silinme işlemi sırasında bir hata ile karşılaşıldı.");
            else return Ok("Başarıyla silindi.");
        }
    }
}
