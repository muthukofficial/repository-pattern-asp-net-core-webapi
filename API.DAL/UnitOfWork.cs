using System;
using System.Threading.Tasks;

namespace API.DAL
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
