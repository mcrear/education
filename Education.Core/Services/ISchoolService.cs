using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface ISchoolService : IService<School>
    {
        new Task<SchoolListResponse> GetAllAsync();
        // new Task<SchoolListResponse> FindAsync(Expression<Func<School, bool>> predicate);
        Task<SchoolListResponse> FindAsync(bool IncludeDeletes);
        Task<SchoolListResponse> FindAsync(SchoolDto schoolDto, bool IncludeDeletes);
        Task<SchoolResponse> AddAsync(SchoolDto schoolDto, Guid userId);
        SchoolResponse Update(SchoolDto schoolDto, Guid userId);
        Task<SchoolResponse> Update(Guid Id, Guid userId);
    }
}
