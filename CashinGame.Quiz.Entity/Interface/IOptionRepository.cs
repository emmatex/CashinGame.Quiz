using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Interface
{
    public interface IOptionRepository : IDisposable
    {
        Task<IEnumerable<Option>> GetAsync();
        Task<Option> GetByIdAsync(Guid id);
        void Add(Option option);
        void Delete(Option option);
        void Update(Option option);
        Task<bool> isExists(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
