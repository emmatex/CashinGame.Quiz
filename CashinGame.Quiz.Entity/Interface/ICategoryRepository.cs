using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Interface
{
    public interface ICategoryRepository : IDisposable
    {
        Task<IEnumerable<Category>> GetAsync();
        Task<Category> GetByIdAsync(Guid id);
        void Add(Category category);
        void Delete(Category category);
        void Update(Category category);
        Task<bool> isExists(string categoryName);
        Task<bool> SaveChangesAsync();
    }
}
