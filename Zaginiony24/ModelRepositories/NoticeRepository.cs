using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zaginiony24.Models;

namespace Zaginiony24.ModelRepositories
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly ApplicationDbContext _context;

        public NoticeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new 
                ArgumentNullException(nameof(context));
        }
        public async Task<Notice> CreateAsync(Notice entity)
        {
            var result = await _context.Notices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Notice> UpdateAsync(Notice entity)
        {
            var result = _context.Notices.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<string> DeleteAsync(Notice entity)
        {
            var result = _context.Notices.Remove(entity);
            await _context.SaveChangesAsync();
            return result.Entity.NoticeId.ToString();
        }

        public async Task<Notice> GetAsync(Guid noticeId)
        {
            return await _context.Notices.FirstOrDefaultAsync(n => n.NoticeId.Equals(noticeId));
        }

        public async Task<List<Notice>> GetAllNotices()
        {
            return await _context.Notices.ToListAsync();
        }

        public Task<List<Notice>> GetNotices(string gender = null, string district = null)
        {
            var queryable = _context.Notices.AsQueryable();
            if (!string.IsNullOrEmpty(gender))
                queryable = queryable.Where(n => n.Gender.Equals(gender));
            if (!string.IsNullOrEmpty(district))
                queryable = queryable.Where(n => n.District.Equals(district));

            return queryable.ToListAsync();
        }

        public async Task<List<Notice>> GetNoticesWthUsers()
        {
            return await _context.Notices.Include(n => n.AppUser).ToListAsync();
        }
    }
}
