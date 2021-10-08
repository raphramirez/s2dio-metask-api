using Domain;
using Domain.Repositories;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlutoContext _context;
        public UnitOfWork(PlutoContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }
        public ITaskRepository Tasks { get; private set; }

        public Task<int> Completed()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
