﻿using DataAccess.DTOs;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class Service<TEntity> : IService<TEntity, int> where TEntity : class
    {
        protected readonly IRepository<TEntity, int> _repository;
        public Service(IRepository<TEntity, int> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TEntity>> GetAll() =>
            await _repository.GetAll();
        public async Task<TEntity> Get(int id) =>
            await _repository.Get(id);
        public async Task<ResponseDto> Add(TEntity tEntity) =>
            await _repository.Add(tEntity);
        public async Task<ResponseDto> Update(TEntity tEntity) =>
            await _repository.Update(tEntity);
        public async Task<ResponseDto> Remove(int id) =>
            await _repository.Remove(id);
    }
}
