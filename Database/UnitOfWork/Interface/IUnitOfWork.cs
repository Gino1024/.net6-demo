using Database.EFCore;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository _userRepository { get; set; }
        public Task<int> SaveChangeAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
