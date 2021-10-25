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
    public class LessonService : Service<Lesson>, ILessonService
    {
        private readonly IMapper mapper;
        public LessonService(IUnitOfWork unitOfWork, IRepository<Lesson> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<LessonResponse> AddAsync(LessonDto lessonDto, Guid userId)
        {
            try
            {
                Lesson lesson = mapper.Map<LessonDto, Lesson>(lessonDto);
                lesson.CreatedBy = userId;
                lesson = await base.AddAsync(lesson);
                lessonDto = mapper.Map<Lesson, LessonDto>(lesson);

                if (lessonDto != null)
                    return new LessonResponse(lessonDto);
                else return new LessonResponse("Yeni ders ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new LessonResponse($"Ders eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<LessonListResponse> GetAllAsync()
        {
            try
            {
                List<LessonDto> Lessons = new List<LessonDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Lessons.Add(mapper.Map<Lesson, LessonDto>(item));
                }
                return new LessonListResponse(Lessons);
            }
            catch (Exception ex)
            {
                return new LessonListResponse($"Dersler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public LessonResponse Update(LessonDto lessonDto, Guid userId)
        {
            try
            {
                Lesson lesson = mapper.Map<LessonDto, Lesson>(lessonDto);
                lesson.UpdatedBy = userId;
                lesson.UpdateDate = DateTime.Now;
                lesson = base.Update(lesson);
                lessonDto = mapper.Map<Lesson, LessonDto>(lesson);

                if (lessonDto != null)
                    return new LessonResponse(lessonDto);
                else return new LessonResponse("Ders güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new LessonResponse($"Ders güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<LessonListResponse> FindAsync(Expression<Func<Lesson, bool>> predicate)
        {
            try
            {
                List<LessonDto> Lessons = new List<LessonDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Lessons.Add(mapper.Map<Lesson, LessonDto>(item));
                }
               return new LessonListResponse(Lessons);
            }
            catch (Exception ex)
            {
                return new LessonListResponse($"Dersler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<LessonListResponse> FindAsync(LessonDto lessonDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression lessonFilter = Expression.Parameter(typeof(Lesson), "s");
                BinaryExpression exp = null;
                if (lessonDto.Id != null && lessonDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(lessonFilter, "Id");
                    var val1 = Expression.Constant(lessonDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (lessonDto.LessonName != null)
                {
                    Expression lessonNameProperty = Expression.Property(lessonFilter, "LessonName");
                    var val2 = Expression.Constant(lessonDto.LessonName);
                    Expression e1 = Expression.Equal(lessonNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (lessonDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(lessonFilter, "IsActive");
                    var val1 = Expression.Constant(lessonDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(lessonFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Lesson, bool>>(exp, lessonFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new LessonListResponse($"Dersler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<LessonListResponse> FindAsync(bool IncludeDeletes)
        {
            LessonDto sdo = new LessonDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<LessonResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Lesson lesson = await base.GetByIdAsync(Id);
                lesson.DeletedBy = userId;
                lesson.DeleteDate = DateTime.Now;
                lesson.IsDeleted = true;
                lesson = base.Update(lesson);
                LessonDto lessonDto = mapper.Map<Lesson, LessonDto>(lesson);

                if (lessonDto != null)
                    return new LessonResponse(lessonDto);
                else return new LessonResponse("Ders silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new LessonResponse($"Ders silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
