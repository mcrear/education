using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Responses;
using Education.Core.Services;
using Education.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class AnswerService : Service<Answer>, IAnswerService
    {
        private readonly IMapper mapper;
        public AnswerService(IUnitOfWork unitOfWork, IRepository<Answer> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<Answer> GetWithQuestionsByIdAsync(Guid Id)
        {
            return await _unitOfWork.Answers.GetWithQuestionsByIdAsync(Id);
        }

        public async Task<AnswerResponse> AddAsync(AnswerDto answerDto, Guid userId)
        {
            try
            {
                Answer answer = mapper.Map<AnswerDto, Answer>(answerDto);
                answer.CreatedBy = userId;
                answer = await base.AddAsync(answer);
                answerDto = mapper.Map<Answer, AnswerDto>(answer);

                if (answerDto != null)
                    return new AnswerResponse(answerDto);
                else return new AnswerResponse("Yeni cevap ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new AnswerResponse($"Cevap eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<_BaseResponse<IEnumerable<Answer>>> GetAllAsync()
        {
            try
            {
                List<AnswerDto> Answers = new List<AnswerDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Answers.Add(mapper.Map<Answer, AnswerDto>(item));
                }

                return new _BaseResponse<IEnumerable<Answer>>(list);
            }
            catch (Exception ex)
            {
                return new _BaseResponse<IEnumerable<Answer>>($"Cevaplar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private async Task<_BaseResponse<IEnumerable<Answer>>> FindFullAsync(Expression<Func<Answer, bool>> predicate)
        {
            try
            {
                List<Answer> Answers = new List<Answer>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Answers.Add(item);
                }
                return new _BaseResponse<IEnumerable<Answer>>(Answers);
            }
            catch (Exception ex)
            {
                return new _BaseResponse<IEnumerable<Answer>>($"Cevaplar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<_BaseResponse<IEnumerable<Answer>>> FindFullAsync(Answer answer)
        {
            try
            {
                ParameterExpression answerFilter = Expression.Parameter(typeof(Answer), "s");
                BinaryExpression exp = null;
                if (answer.Id != null && answer.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(answerFilter, "Id");
                    var val1 = Expression.Constant(answer.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (answer.AnswerText != null)
                {
                    Expression answerNameProperty = Expression.Property(answerFilter, "AnswerText");
                    var val2 = Expression.Constant(answer.AnswerText);
                    Expression e1 = Expression.Equal(answerNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (answer.IsActive)
                {
                    Expression isActiveProperty = Expression.Property(answerFilter, "IsActive");
                    var val1 = Expression.Constant(answer.IsActive);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (answer.IsDeleted)
                {
                    Expression isDeletedProperty = Expression.Property(answerFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Answer, bool>>(exp, answerFilter);
                return await FindFullAsync(lambda);
            }
            catch (Exception ex)
            {
                return new _BaseResponse<IEnumerable<Answer>>($"Cevaplar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public AnswerResponse Update(AnswerDto answerDto, Guid userId)
        {
            try
            {
                Answer answer = mapper.Map<AnswerDto, Answer>(answerDto);
                answer.UpdatedBy = userId;
                answer.UpdateDate = DateTime.Now;
                answer = base.Update(answer);
                answerDto = mapper.Map<Answer, AnswerDto>(answer);

                if (answerDto != null)
                    return new AnswerResponse(answerDto);
                else return new AnswerResponse("Cevap güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new AnswerResponse($"Cevap güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<AnswerListResponse> FindAsync(Expression<Func<Answer, bool>> predicate)
        {
            try
            {
                List<AnswerDto> Answers = new List<AnswerDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Answers.Add(mapper.Map<Answer, AnswerDto>(item));
                }
                return new AnswerListResponse(Answers);
            }
            catch (Exception ex)
            {
                return new AnswerListResponse($"Cevaplar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<AnswerListResponse> FindAsync(AnswerDto answerDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression answerFilter = Expression.Parameter(typeof(Answer), "s");
                BinaryExpression exp = null;
                if (answerDto.Id != null && answerDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(answerFilter, "Id");
                    var val1 = Expression.Constant(answerDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (answerDto.AnswerText != null)
                {
                    Expression answerNameProperty = Expression.Property(answerFilter, "AnswerText");
                    var val2 = Expression.Constant(answerDto.AnswerText);
                    Expression e1 = Expression.Equal(answerNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (answerDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(answerFilter, "IsActive");
                    var val1 = Expression.Constant(answerDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(answerFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Answer, bool>>(exp, answerFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new AnswerListResponse($"Cevaplar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<AnswerListResponse> FindAsync(bool IncludeDeletes)
        {
            AnswerDto sdo = new AnswerDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<AnswerResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Answer answer = await base.GetByIdAsync(Id);
                answer.DeletedBy = userId;
                answer.DeleteDate = DateTime.Now;
                answer.IsDeleted = true;
                answer = base.Update(answer);
                AnswerDto answerDto = mapper.Map<Answer, AnswerDto>(answer);

                if (answerDto != null)
                    return new AnswerResponse(answerDto);
                else return new AnswerResponse("Cevap silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new AnswerResponse($"Cevap silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
