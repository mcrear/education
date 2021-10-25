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
    public class ExamService : Service<Exam>, IExamService
    {
        private readonly IMapper mapper;
        public ExamService(IUnitOfWork unitOfWork, IRepository<Exam> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<ExamResponse> AddAsync(ExamDto examDto, Guid userId)
        {
            try
            {
                Exam exam = mapper.Map<ExamDto, Exam>(examDto);
                exam.CreatedBy = userId;
                exam = await base.AddAsync(exam);
                examDto = mapper.Map<Exam, ExamDto>(exam);

                if (examDto != null)
                    return new ExamResponse(examDto);
                else return new ExamResponse("Yeni sınav ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ExamResponse($"Sınav eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<ExamListResponse> GetAllAsync()
        {
            try
            {
                List<ExamDto> Exams = new List<ExamDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Exams.Add(mapper.Map<Exam, ExamDto>(item));
                }
                return new ExamListResponse(Exams);
            }
            catch (Exception ex)
            {
                return new ExamListResponse($"Sınavlar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public ExamResponse Update(ExamDto examDto, Guid userId)
        {
            try
            {
                Exam exam = mapper.Map<ExamDto, Exam>(examDto);
                exam.UpdatedBy = userId;
                exam.UpdateDate = DateTime.Now;
                exam = base.Update(exam);
                examDto = mapper.Map<Exam, ExamDto>(exam);

                if (examDto != null)
                    return new ExamResponse(examDto);
                else return new ExamResponse("Sınav güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ExamResponse($"Sınav güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<ExamListResponse> FindAsync(Expression<Func<Exam, bool>> predicate)
        {
            try
            {
                List<ExamDto> Exams = new List<ExamDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Exams.Add(mapper.Map<Exam, ExamDto>(item));
                }
                return new ExamListResponse(Exams);
            }
            catch (Exception ex)
            {
                return new ExamListResponse($"Sınavlar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<ExamListResponse> FindAsync(ExamDto examDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression examFilter = Expression.Parameter(typeof(Exam), "s");
                BinaryExpression exp = null;
                if (examDto.Id != null && examDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(examFilter, "Id");
                    var val1 = Expression.Constant(examDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (examDto.ExamName != null)
                {
                    Expression examNameProperty = Expression.Property(examFilter, "ExamName");
                    var val2 = Expression.Constant(examDto.ExamName);
                    Expression e1 = Expression.Equal(examNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (examDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(examFilter, "IsActive");
                    var val1 = Expression.Constant(examDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(examFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Exam, bool>>(exp, examFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new ExamListResponse($"Sınavlar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<ExamListResponse> FindAsync(bool IncludeDeletes)
        {
            ExamDto sdo = new ExamDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<ExamResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Exam exam = await base.GetByIdAsync(Id);
                exam.DeletedBy = userId;
                exam.DeleteDate = DateTime.Now;
                exam.IsDeleted = true;
                exam = base.Update(exam);
                ExamDto examDto = mapper.Map<Exam, ExamDto>(exam);

                if (examDto != null)
                    return new ExamResponse(examDto);
                else return new ExamResponse("Sınav silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ExamResponse($"Sınav silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
