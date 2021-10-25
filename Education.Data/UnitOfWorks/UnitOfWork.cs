using Education.Core.Repositories;
using Education.Core.UnitOfWorks;
using Education.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IQuestionRepository Questions => _questionRepository ?? new QuestionRepository(_context);

        public IAnswerRepository Answers => _answerRepository ?? new AnswerRepository(_context);

        public IClassroomRepository Classrooms => _classroomRepository ?? new ClassroomRepository(_context);

        public IExamRepository Exams => _examRepository ?? new ExamRepository(_context);

        public ILessonRepository Lessons => _lessonRepository ?? new LessonRepository(_context);

        public IPermissionRepository Permissions => _permissionRepository ?? new PermissionRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new RoleRepository(_context);

        public ISchoolRepository Schools => _schoolRepository ?? new SchoolRepository(_context);

        public ITopicRepository Topics => _topicRepository ?? new TopicRepository(_context);

        public IUserRepository Users => _userRepository ?? new UserRepository(_context);

        public IQuestionTypeRepository QuestionTypes => _questionTypeRepository ?? new QuestionTypeRepository(_context);

        private readonly AppDbContext _context;
        private QuestionRepository _questionRepository;
        private QuestionTypeRepository _questionTypeRepository;
        private AnswerRepository _answerRepository;
        private ClassroomRepository _classroomRepository;
        private ExamRepository _examRepository;
        private LessonRepository _lessonRepository;
        private PermissionRepository _permissionRepository;
        private RoleRepository _roleRepository;
        private SchoolRepository _schoolRepository;
        private TopicRepository _topicRepository;
        private UserRepository _userRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
