using Education.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IQuestionRepository Questions { get; }
        IQuestionTypeRepository QuestionTypes { get; }
        IAnswerRepository Answers { get; }
        IClassroomRepository Classrooms { get; }
        IExamRepository Exams { get; }
        ILessonRepository Lessons { get; }
        IPermissionRepository Permissions { get; }
        IRoleRepository Roles { get; }
        ISchoolRepository Schools { get; }
        ITopicRepository Topics { get; }
        IUserRepository Users { get; }

        Task CommitAsync();
        void Commit();
    }
}
