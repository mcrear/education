using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IExamService : IService<Exam>
    {
        new Task<ExamListResponse> GetAllAsync();
        // new Task<ExamListResponse> FindAsync(Expression<Func<Exam, bool>> predicate);
        Task<ExamListResponse> FindAsync(bool IncludeDeletes);
        Task<ExamListResponse> FindAsync(ExamDto examDto, bool IncludeDeletes);
        Task<ExamResponse> AddAsync(ExamDto examDto, Guid userId);
        ExamResponse Update(ExamDto examDto, Guid userId);
        Task<ExamResponse> Update(Guid Id, Guid userId);
    }
}
