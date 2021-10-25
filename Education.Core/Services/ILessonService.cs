using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface ILessonService : IService<Lesson>
    {
        new Task<LessonListResponse> GetAllAsync();
        // new Task<LessonListResponse> FindAsync(Expression<Func<Lesson, bool>> predicate);
        Task<LessonListResponse> FindAsync(bool IncludeDeletes);
        Task<LessonListResponse> FindAsync(LessonDto lessonDto, bool IncludeDeletes);
        Task<LessonResponse> AddAsync(LessonDto lessonDto, Guid userId);
        LessonResponse Update(LessonDto lessonDto, Guid userId);
        Task<LessonResponse> Update(Guid Id, Guid userId);
    }
}
