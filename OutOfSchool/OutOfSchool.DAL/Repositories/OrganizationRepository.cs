using Microsoft.EntityFrameworkCore;
using OutOfSchool.DAL.Interfaces;
using OutOfSchool.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OutOfSchool.DAL.Repositories
{
    class OrganizationRepository : IRepository<Organization>
    {
        private readonly MainContext _context;

        public OrganizationRepository(MainContext context)
        {
            _context = context;
        }

        private async Task<bool> OrganizationExists(int id)
        {
            return await GetByIdAsync(id) != null;
        }

        public async Task<Organization> AddAsync(Organization entity)
        {
            _context.Organizations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await OrganizationExists(id))
                return false;
            var toRemove = _context.Organizations.Find(id);
            _context.Organizations.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true; ;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<Organization> GetByIdAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Organization entity)
        {
            if (!await OrganizationExists(entity.Id))
                return false;
            _context.Organizations.Update(entity);

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
