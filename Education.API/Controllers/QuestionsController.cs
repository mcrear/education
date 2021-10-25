using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Education.Core.Request;
using Education.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Education.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;

        public QuestionsController(IQuestionService questionService, IAnswerService answerService, IMapper mapper)
        {
            this.mapper = mapper;
            this.questionService = questionService;
            this.answerService = answerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetQuestionWithAnswer(Guid Id)
        {
            var question = await questionService.GetWithAnswersByIdAsync(Id);
            if (question != null) return Created("", question); else return BadRequest(question);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(QuestionRequest questionRequest)
        {
            IEnumerable<Claim> claims = User.Claims;
            Guid userId = new Guid(claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            var question = await questionService.AddWithAnswersAsync(questionRequest, userId);
            if (question != null) return Created("", question); else return BadRequest(question);
        }

    }
}
