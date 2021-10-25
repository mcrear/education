using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IClassroomService : IService<Classroom>
    {
        new Task<ClassroomListResponse> GetAllAsync();
        // new Task<ClassroomListResponse> FindAsync(Expression<Func<Classroom, bool>> predicate);
        Task<ClassroomListResponse> FindAsync(bool IncludeDeletes);
        Task<ClassroomListResponse> FindAsync(ClassroomDto classroomDto, bool IncludeDeletes);
        Task<ClassroomResponse> AddAsync(ClassroomDto classroomDto, Guid userId);
        ClassroomResponse Update(ClassroomDto classroomDto, Guid userId);
        Task<ClassroomResponse> Update(Guid Id, Guid userId);
    }
}
