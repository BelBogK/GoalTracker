using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Interfaces.Repositories
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string userId);
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
