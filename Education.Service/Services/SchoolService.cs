using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Responses;
using Education.Core.Services;
using Education.Core.UnitOfWorks;

namespace Education.Service.Services
{
    public class SchoolService : Service<School>, ISchoolService
    {
        private readonly IMapper mapper;
        public SchoolService(IUnitOfWork unitOfWork, IRepository<School> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<SchoolResponse> AddAsync(SchoolDto schoolDto, Guid userId)
        {
            try
            {
                School school = mapper.Map<SchoolDto, School>(schoolDto);
                school.CreatedBy = userId;
                school = await base.AddAsync(school);
                schoolDto = mapper.Map<School, SchoolDto>(school);

                if (schoolDto != null)
                    return new SchoolResponse(schoolDto);
                else return new SchoolResponse("Yeni okul ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new SchoolResponse($"Okul eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<SchoolListResponse> GetAllAsync()
        {
            try
            {
                List<SchoolDto> Schools = new List<SchoolDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Schools.Add(mapper.Map<School, SchoolDto>(item));
                }
               return new SchoolListResponse(Schools);
            }
            catch (Exception ex)
            {
                return new SchoolListResponse($"Okullar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public SchoolResponse Update(SchoolDto schoolDto, Guid userId)
        {
            try
            {
                School school = mapper.Map<SchoolDto, School>(schoolDto);
                school.UpdatedBy = userId;
                school.UpdateDate = DateTime.Now;
                school = base.Update(school);
                schoolDto = mapper.Map<School, SchoolDto>(school);

                if (schoolDto != null)
                    return new SchoolResponse(schoolDto);
                else return new SchoolResponse("Okul güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new SchoolResponse($"Okul güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<SchoolListResponse> FindAsync(Expression<Func<School, bool>> predicate)
        {
            try
            {
                List<SchoolDto> Schools = new List<SchoolDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Schools.Add(mapper.Map<School, SchoolDto>(item));
                }
               return new SchoolListResponse(Schools);
            }
            catch (Exception ex)
            {
                return new SchoolListResponse($"Okullar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<SchoolListResponse> FindAsync(SchoolDto schoolDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression schoolFilter = Expression.Parameter(typeof(School), "s");
                BinaryExpression exp = null;
                if (schoolDto.Id != null && schoolDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(schoolFilter, "Id");
                    var val1 = Expression.Constant(schoolDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (schoolDto.SchoolName != null)
                {
                    Expression schoolNameProperty = Expression.Property(schoolFilter, "SchoolName");
                    var val2 = Expression.Constant(schoolDto.SchoolName);
                    Expression e1 = Expression.Equal(schoolNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (schoolDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(schoolFilter, "IsActive");
                    var val1 = Expression.Constant(schoolDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(schoolFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<School, bool>>(exp, schoolFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new SchoolListResponse($"Okullar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<SchoolListResponse> FindAsync(bool IncludeDeletes)
        {
            SchoolDto sdo = new SchoolDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<SchoolResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                School school = await base.GetByIdAsync(Id);
                school.DeletedBy = userId;
                school.DeleteDate = DateTime.Now;
                school.IsDeleted = true;
                school = base.Update(school);
                SchoolDto schoolDto = mapper.Map<School, SchoolDto>(school);

                if (schoolDto != null)
                    return new SchoolResponse(schoolDto);
                else return new SchoolResponse("Okul silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new SchoolResponse($"Okul silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
