﻿using CashinGame.Quiz.Entity.Infrastructure;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Entity.Repository
{
    public class QuestionRepository : IQuestionRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            question.Id = Guid.NewGuid();
            question.CreatedOn = DateTimeOffset.Now.DateTime;
            _context.Questions.Add(question);
        }

        public void Delete(Question question)
        {
            _context.Questions.Remove(question);
        }

        public async Task<IEnumerable<Question>> GetAsync()
        {
            return await _context.Questions.OrderBy(x => x.QuestionText).ToListAsync();
        }

        public async Task<Question> GetByIdAsync(Guid id)
        {
            return await _context.Questions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> isExists(Guid id)
        {
            return await _context.Questions.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Update(Question question)
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