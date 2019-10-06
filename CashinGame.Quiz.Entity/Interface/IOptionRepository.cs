using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Interface
{
    public interface IOptionRepository : IDisposable
    {
        Task<IEnumerable<Option>> GetOptionAsync(Guid questionId);
        Task<Option> GetOptionAsync(Guid questionId, Guid optionId);
        void Add(Guid questionId, Option option);
        void Delete(Option option);
        void Update(Option option);
        Task<bool> isExists(Guid questionId, string value);
        Task<bool> SaveChangesAsync();    
    }
}
