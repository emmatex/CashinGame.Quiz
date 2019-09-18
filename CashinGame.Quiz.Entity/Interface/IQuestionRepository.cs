using CashinGame.Quiz.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Interface
{
    public interface IQuestionRepository : IDisposable
    {
        Task<IEnumerable<Question>> GetAsync();
        Task<Question> GetByIdAsync(Guid id);
        void Add(Question question);
        void Delete(Question question);
        void Update(Question question);
        Task<bool> isExists(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
