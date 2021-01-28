using Microsoft.EntityFrameworkCore;
using OutOfSchool.DAL.Interfaces;
using OutOfSchool.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfSchool.DAL.Repositories
{
    class ParentRepository : IRepository<Parent>
    {
        private readonly MainContext _context;

        public ParentRepository(MainContext context)
        {
            _context = context;
        }

        private async Task<bool> ParentExists(int id)
        {
            return await GetByIdAsync(id) != null;
        }

        public async Task<Parent> AddAsync(Parent entity)
        {
            _context.Parents.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await ParentExists(id))
                return false;
            var toRemove = _context.Parents.Find(id);
            _context.Parents.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true; ;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Parent>> GetAllAsync()
        {
            return await _context.Parents.ToListAsync();
        }

        public async Task<Parent> GetByIdAsync(int id)
        {
            return await _context.Parents.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Parent entity)
        {
            if (!await ParentExists(entity.Id))
                return false;
            _context.Parents.Update(entity);

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
