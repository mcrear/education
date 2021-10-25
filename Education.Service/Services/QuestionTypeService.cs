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
    public class QuestionTypeService : Service<QuestionType>, IQuestionTypeService
    {

        private readonly IMapper mapper;
        public QuestionTypeService(IUnitOfWork unitOfWork, IRepository<QuestionType> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<QuestionTypeResponse> AddAsync(QuestionTypeDto questionTypeDto, Guid userId)
        {
            try
            {
                QuestionType questionType = mapper.Map<QuestionTypeDto, QuestionType>(questionTypeDto);
                questionType.CreatedBy = userId;
                questionType = await base.AddAsync(questionType);
                questionTypeDto = mapper.Map<QuestionType, QuestionTypeDto>(questionType);

                if (questionTypeDto != null)
                    return new QuestionTypeResponse(questionTypeDto);
                else return new QuestionTypeResponse("Yeni soru tipi ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new QuestionTypeResponse($"Soru Tipi eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<QuestionTypeListResponse> GetAllAsync()
        {
            try
            {
                List<QuestionTypeDto> QuestionTypes = new List<QuestionTypeDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    QuestionTypes.Add(mapper.Map<QuestionType, QuestionTypeDto>(item));
                }
                return new QuestionTypeListResponse(QuestionTypes);
            }
            catch (Exception ex)
            {
                return new QuestionTypeListResponse($"Soru Tipleri listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public QuestionTypeResponse Update(QuestionTypeDto questionTypeDto, Guid userId)
        {
            try
            {
                QuestionType questionType = mapper.Map<QuestionTypeDto, QuestionType>(questionTypeDto);
                questionType.UpdatedBy = userId;
                questionType.UpdateDate = DateTime.Now;
                questionType = base.Update(questionType);
                questionTypeDto = mapper.Map<QuestionType, QuestionTypeDto>(questionType);

                if (questionTypeDto != null)
                    return new QuestionTypeResponse(questionTypeDto);
                else return new QuestionTypeResponse("Soru Tipi güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new QuestionTypeResponse($"Soru Tipi güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<QuestionTypeListResponse> FindAsync(Expression<Func<QuestionType, bool>> predicate)
        {
            try
            {
                List<QuestionTypeDto> QuestionTypes = new List<QuestionTypeDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    QuestionTypes.Add(mapper.Map<QuestionType, QuestionTypeDto>(item));
                }
               return new QuestionTypeListResponse(QuestionTypes);
            }
            catch (Exception ex)
            {
                return new QuestionTypeListResponse($"Soru Tipleri listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<QuestionTypeListResponse> FindAsync(QuestionTypeDto questionTypeDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression questionTypeFilter = Expression.Parameter(typeof(QuestionType), "s");
                BinaryExpression exp = null;
                if (questionTypeDto.Id != null && questionTypeDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(questionTypeFilter, "Id");
                    var val1 = Expression.Constant(questionTypeDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (questionTypeDto.TypeName != null)
                {
                    Expression questionTypeNameProperty = Expression.Property(questionTypeFilter, "TypeName");
                    var val2 = Expression.Constant(questionTypeDto.TypeName);
                    Expression e1 = Expression.Equal(questionTypeNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (questionTypeDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(questionTypeFilter, "IsActive");
                    var val1 = Expression.Constant(questionTypeDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(questionTypeFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<QuestionType, bool>>(exp, questionTypeFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new QuestionTypeListResponse($"Soru Tipleri listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<QuestionTypeListResponse> FindAsync(bool IncludeDeletes)
        {
            QuestionTypeDto sdo = new QuestionTypeDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<QuestionTypeResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                QuestionType questionType = await base.GetByIdAsync(Id);
                questionType.DeletedBy = userId;
                questionType.DeleteDate = DateTime.Now;
                questionType.IsDeleted = true;
                questionType = base.Update(questionType);
                QuestionTypeDto questionTypeDto = mapper.Map<QuestionType, QuestionTypeDto>(questionType);

                if (questionTypeDto != null)
                    return new QuestionTypeResponse(questionTypeDto);
                else return new QuestionTypeResponse("Soru Tipi silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new QuestionTypeResponse($"Soru Tipi silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
