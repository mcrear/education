using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //CreateMap<Answer, AnswerDto>();
            //CreateMap<AnswerDto, Answer>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<School, SchoolDto>();
            CreateMap<SchoolDto, School>();

            CreateMap<QuestionType, QuestionTypeDto>();
            CreateMap<QuestionTypeDto, QuestionType>();

            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();

            CreateMap<Classroom, ClassroomDto>();
            CreateMap<ClassroomDto, Classroom>();

            CreateMap<Exam, ExamDto>();
            CreateMap<ExamDto, Exam>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();

            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();

            CreateMap<Topic, TopicDto>();
            CreateMap<TopicDto, Topic>();
        }
    }
}
