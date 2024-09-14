using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity, new()
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync()
        {
            return Task.FromResult(new T
            {
                Id = Guid.NewGuid()
            });
        }

        public Task<bool> AddAsync(T entity)
        {
            var isContains = Data.Contains(entity);

            if (!isContains)
            {
                var data = Data as List<T>;

                data.Add(entity);
            }

            return Task.FromResult(isContains);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var isContains = Data.Any(x => x.Id == id);

            if (isContains)
            {
                var data = Data as List<T>;
                var removeItem = await GetByIdAsync(id);

                data.Remove(removeItem);
            }

            return isContains;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await DeleteByIdAsync(entity.Id);
            await AddAsync(entity);

            return entity;
        }
    }
}