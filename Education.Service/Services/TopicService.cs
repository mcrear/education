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
    public class TopicService : Service<Topic>, ITopicService
    {
        private readonly IMapper mapper;
        public TopicService(IUnitOfWork unitOfWork, IRepository<Topic> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<TopicResponse> AddAsync(TopicDto topicDto, Guid userId)
        {
            try
            {
                Topic topic = mapper.Map<TopicDto, Topic>(topicDto);
                topic.CreatedBy = userId;
                topic = await base.AddAsync(topic);
                topicDto = mapper.Map<Topic, TopicDto>(topic);

                if (topicDto != null)
                    return new TopicResponse(topicDto);
                else return new TopicResponse("Yeni konu ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new TopicResponse($"Konu eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<TopicListResponse> GetAllAsync()
        {
            try
            {
                List<TopicDto> Topics = new List<TopicDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Topics.Add(mapper.Map<Topic, TopicDto>(item));
                }
               return new TopicListResponse(Topics);
            }
            catch (Exception ex)
            {
                return new TopicListResponse($"Konular listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public TopicResponse Update(TopicDto topicDto, Guid userId)
        {
            try
            {
                Topic topic = mapper.Map<TopicDto, Topic>(topicDto);
                topic.UpdatedBy = userId;
                topic.UpdateDate = DateTime.Now;
                topic = base.Update(topic);
                topicDto = mapper.Map<Topic, TopicDto>(topic);

                if (topicDto != null)
                    return new TopicResponse(topicDto);
                else return new TopicResponse("Konu güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new TopicResponse($"Konu güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<TopicListResponse> FindAsync(Expression<Func<Topic, bool>> predicate)
        {
            try
            {
                List<TopicDto> Topics = new List<TopicDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Topics.Add(mapper.Map<Topic, TopicDto>(item));
                }
                return new TopicListResponse(Topics);
            }
            catch (Exception ex)
            {
                return new TopicListResponse($"Konular listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<TopicListResponse> FindAsync(TopicDto topicDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression topicFilter = Expression.Parameter(typeof(Topic), "s");
                BinaryExpression exp = null;
                if (topicDto.Id != null && topicDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(topicFilter, "Id");
                    var val1 = Expression.Constant(topicDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (topicDto.TopicName != null)
                {
                    Expression topicNameProperty = Expression.Property(topicFilter, "TopicName");
                    var val2 = Expression.Constant(topicDto.TopicName);
                    Expression e1 = Expression.Equal(topicNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (topicDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(topicFilter, "IsActive");
                    var val1 = Expression.Constant(topicDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(topicFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Topic, bool>>(exp, topicFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new TopicListResponse($"Konular listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<TopicListResponse> FindAsync(bool IncludeDeletes)
        {
            TopicDto sdo = new TopicDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<TopicResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Topic topic = await base.GetByIdAsync(Id);
                topic.DeletedBy = userId;
                topic.DeleteDate = DateTime.Now;
                topic.IsDeleted = true;
                topic = base.Update(topic);
                TopicDto topicDto = mapper.Map<Topic, TopicDto>(topic);

                if (topicDto != null)
                    return new TopicResponse(topicDto);
                else return new TopicResponse("Konu silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new TopicResponse($"Konu silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
