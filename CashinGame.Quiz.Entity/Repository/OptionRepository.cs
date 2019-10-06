using CashinGame.Quiz.Entity.Infrastructure;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Repository
{
    public class OptionRepository : IOptionRepository, IDisposable
    {
        private ApplicationDbContext _context;
        private readonly ILogger<OptionRepository> _logger;

        public OptionRepository(ApplicationDbContext context, ILogger<OptionRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<IEnumerable<Option>> GetOptionAsync(Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                throw new ArgumentException(nameof(questionId));
            }

            return await _context.Options.Include(x => x.Question)
                .Where(x => x.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task<Option> GetOptionAsync(Guid questionId, Guid optionId)
        {
            if (questionId == Guid.Empty)
            {
                throw new ArgumentException(nameof(questionId));
            }

            if (optionId == Guid.Empty)
            {
                throw new ArgumentException(nameof(optionId));
            }

            return await _context.Options.Include(x => x.Question)
                .Where(x => x.QuestionId == questionId && x.Id == optionId)
                .FirstOrDefaultAsync();
        }

        public void Add(Guid questionId, Option option)
        {
            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }

            if(questionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(questionId));
            }

            option.Id = Guid.NewGuid();
            option.QuestionId = questionId;
            option.CreatedOn = DateTimeOffset.Now.DateTime;
            _context.Options.Add(option);
        }

        public void Delete(Option option)
        {
            _context.Options.Remove(option);
        }

        public async Task<IEnumerable<Option>> GetAsync()
        {
            return await _context.Options.OrderBy(x => x.Label).ToListAsync();
        }

        public async Task<Option> GetByIdAsync(Guid id)
        {
            return await _context.Options.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> isExists(Guid questionId, string value)
        {
            return await _context.Options.AnyAsync(a => a.QuestionId == questionId && a.Value == value);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Option option)
        {
            // no code in this implementation
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

    }
}
