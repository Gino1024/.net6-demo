using Database.EFCore;
using Database.Repository.Implement.EF;
using Database.Repository.Interface;
using Database.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace Database.UnitOfWork.Implement
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private DemoContext _demoContext;
        public IUserRepository _userRepository { get; set; }


        public EFUnitOfWork(DemoContext demoContext, IUserRepository userRepository)
        {
            _demoContext = demoContext;
            _userRepository = userRepository;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _demoContext.Database.BeginTransactionAsync();
        }

        
        public async Task<int> SaveChangeAsync()
        {
          return await _demoContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _demoContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }

}
