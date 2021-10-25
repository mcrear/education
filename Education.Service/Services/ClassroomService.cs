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
    public class ClassroomService : Service<Classroom>, IClassroomService
    {
        private readonly IMapper mapper;
        public ClassroomService(IUnitOfWork unitOfWork, IRepository<Classroom> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<ClassroomResponse> AddAsync(ClassroomDto classroomDto, Guid userId)
        {
            try
            {
                Classroom classroom = mapper.Map<ClassroomDto, Classroom>(classroomDto);
                classroom.CreatedBy = userId;
                classroom = await base.AddAsync(classroom);
                classroomDto = mapper.Map<Classroom, ClassroomDto>(classroom);

                if (classroomDto != null)
                    return new ClassroomResponse(classroomDto);
                else return new ClassroomResponse("Yeni sınıf ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ClassroomResponse($"Sınıf eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<ClassroomListResponse> GetAllAsync()
        {
            try
            {
                List<ClassroomDto> Classrooms = new List<ClassroomDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Classrooms.Add(mapper.Map<Classroom, ClassroomDto>(item));
                }
                return new ClassroomListResponse(Classrooms);
            }
            catch (Exception ex)
            {
                return new ClassroomListResponse($"Sınıflar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public ClassroomResponse Update(ClassroomDto classroomDto, Guid userId)
        {
            try
            {
                Classroom classroom = mapper.Map<ClassroomDto, Classroom>(classroomDto);
                classroom.UpdatedBy = userId;
                classroom.UpdateDate = DateTime.Now;
                classroom = base.Update(classroom);
                classroomDto = mapper.Map<Classroom, ClassroomDto>(classroom);

                if (classroomDto != null)
                    return new ClassroomResponse(classroomDto);
                else return new ClassroomResponse("Sınıf güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ClassroomResponse($"Sınıf güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<ClassroomListResponse> FindAsync(Expression<Func<Classroom, bool>> predicate)
        {
            try
            {
                List<ClassroomDto> Classrooms = new List<ClassroomDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Classrooms.Add(mapper.Map<Classroom, ClassroomDto>(item));
                }
             return new ClassroomListResponse(Classrooms);
            }
            catch (Exception ex)
            {
                return new ClassroomListResponse($"Sınıflar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<ClassroomListResponse> FindAsync(ClassroomDto classroomDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression classroomFilter = Expression.Parameter(typeof(Classroom), "s");
                BinaryExpression exp = null;
                if (classroomDto.Id != null && classroomDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(classroomFilter, "Id");
                    var val1 = Expression.Constant(classroomDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (classroomDto.ClassName != null)
                {
                    Expression classroomNameProperty = Expression.Property(classroomFilter, "ClassName");
                    var val2 = Expression.Constant(classroomDto.ClassName);
                    Expression e1 = Expression.Equal(classroomNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (classroomDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(classroomFilter, "IsActive");
                    var val1 = Expression.Constant(classroomDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(classroomFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Classroom, bool>>(exp, classroomFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new ClassroomListResponse($"Sınıflar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<ClassroomListResponse> FindAsync(bool IncludeDeletes)
        {
            ClassroomDto sdo = new ClassroomDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<ClassroomResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Classroom classroom = await base.GetByIdAsync(Id);
                classroom.DeletedBy = userId;
                classroom.DeleteDate = DateTime.Now;
                classroom.IsDeleted = true;
                classroom = base.Update(classroom);
                ClassroomDto classroomDto = mapper.Map<Classroom, ClassroomDto>(classroom);

                if (classroomDto != null)
                    return new ClassroomResponse(classroomDto);
                else return new ClassroomResponse("Sınıf silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new ClassroomResponse($"Sınıf silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
