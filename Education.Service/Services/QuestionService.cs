using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Request;
using Education.Core.Responses;
using Education.Core.Services;
using Education.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class QuestionService : Service<Question>, IQuestionService
    {
        private readonly IMapper mapper;
        public QuestionService(IUnitOfWork unitOfWork, IRepository<Question> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<QuestionResponse> AddWithAnswersAsync(QuestionRequest questionRequest, Guid UserId)
        {
            Guid questionId = Guid.Empty;
            try
            {
                QuestionDto questionDto = new QuestionDto();
                var question = await _unitOfWork.Questions.AddAsync(new Question
                {
                    QuestionText = questionRequest.QuestionText,
                    TagText = questionRequest.TagText,
                    CreatedBy = UserId,
                    QuestionTypeId = questionRequest.QuestionTypeId
                });
                questionId = question.Id;
                _unitOfWork.Commit();
                questionDto.Id = question.Id;
                questionDto.QuestionText = question.QuestionText;
                questionDto.TagText = question.TagText;
                Guid RightAnswer = Guid.Empty;

                bool signRightAnswer = false;

                foreach (var item in questionRequest.Answers)
                {
                    if (item.AnswerText.StartsWith("---"))
                        signRightAnswer = true;

                    var answer = await _unitOfWork.Answers.AddAsync(new Answer
                    {
                        AnswerText = item.AnswerText.Replace("---", ""),
                        QuestionId = question.Id,
                        CreatedBy = UserId
                    });

                    questionDto.Answers.Add(new AnswerDto
                    {
                        AnswerText = answer.AnswerText,
                        Id = answer.Id
                    });
                    if (signRightAnswer)
                        RightAnswer = answer.Id;
                }
                question.RightAnswer = RightAnswer;
                questionDto.RightAnswer = question.RightAnswer;
                if (question.RightAnswer != null)
                {
                    question = _unitOfWork.Questions.Update(question);
                    await _unitOfWork.CommitAsync();
                    return new QuestionResponse(questionDto);
                }
                else
                {
                    return new QuestionResponse("Doğru cevap eklenemedi.");
                }
            }
            catch (Exception ex)
            {
                var question = await GetByIdAsync(questionId);
                _unitOfWork.Questions.Remove(question);
                return new QuestionResponse($"Soru eklenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<QuestionResponse> GetWithAnswersByIdAsync(Guid Id)
        {
            try
            {
                var question = await _unitOfWork.Questions.GetWithAnswersByIdAsync(Id);
                QuestionDto questionDto = new QuestionDto();
                questionDto.Id = question.Id;
                questionDto.QuestionText = question.QuestionText;
                questionDto.RightAnswer = question.RightAnswer;
                questionDto.TagText = question.TagText;
                questionDto.Answers = new List<AnswerDto>();
                foreach (var item in question.Answers)
                {
                    questionDto.Answers.Add(new AnswerDto { AnswerText = item.AnswerText, Id = item.Id });
                }
                if (questionDto != null && questionDto.Answers.Count > 0)
                    return new QuestionResponse(questionDto);
                else
                    return new QuestionResponse("Soru hazırlanırken bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new QuestionResponse($"Soru bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

    }
}
