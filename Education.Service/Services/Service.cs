using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Services;
using Education.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : _BaseEntity
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        public Service(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entites)
        {
            await _repository.AddRangeAsync(entites);
            await _unitOfWork.CommitAsync();
            return entites;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.GetByIdAsync(Id);
        }

        public virtual void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _unitOfWork.Commit();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entites)
        {
            _repository.RemoveRange(entites);
            _unitOfWork.CommitAsync();
        }

        public virtual async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.SingleOrDefault(predicate);
        }

        public virtual TEntity Update(TEntity entity)
        {
            var updatedEntity = _repository.Update(entity);
            _unitOfWork.Commit();
            return updatedEntity;
        }
    }
}
