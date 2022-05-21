using Database.EFCore;
using Database.Models.DTO.ParameterDto;
using Database.Models.DTO.ResultDto;
using Database.Models.SQLModels;
using Database.Repository.Interface;
using Infrastructrue.Extension;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implement.EF
{
    public class UserRepository : GenericRepository<TUser>, IUserRepository
    {
        public UserRepository(DemoContext context) : base(context)
        {
        }

        public async Task<GetListAccountResultDto> GetListAccountAsync(GetListAccountParameterDto instance)
        {
            var result = new GetListAccountResultDto();
            IQueryable<TUser> query = _context.TUsers;

            #region 查詢
            if (!string.IsNullOrEmpty(instance.UserName))
            {
                query = query.Where(m => m.UserName.Contains(instance.UserName));
            }
            if (!string.IsNullOrEmpty(instance.Email))
            {
                query = query.Where(m => m.Account.Contains(instance.Email));
            }
            if (instance.GroupID.HasValue)
            {
                query = query.Where(m => m.GroupId == instance.GroupID);
            }
            if (instance.CreatedAtStart.HasValue)
            {
                query = query.Where(m => m.CreateAt >= instance.CreatedAtStart);
            }
            if (instance.CreatedAtEnd.HasValue)
            {
                query = query.Where(m => m.CreateAt <= instance.CreatedAtEnd);
            }
            if (instance.UpdateAtStart.HasValue)
            {
                query = query.Where(m => m.UpdateAt >= instance.UpdateAtStart);
            }
            if (instance.UpdateAtEnd.HasValue)
            {
                query = query.Where(m => m.UpdateAt >= instance.UpdateAtEnd);
            }          
            if (!string.IsNullOrEmpty(instance.OrderBy))
            {
                query = query.Sort(instance.OrderBy);
            }
            #endregion

            #region 分頁
            int totalCount = query.Count();
            result.SetPaging(totalCount,instance.PageOfCount,instance.Page);
            query = query.Skip(result.PageOfCount.Value * (result.Page.Value - 1)).Take(result.PageOfCount.Value);
            #endregion

            #region 執行查詢
            result.data = await query.Select(m => new AccountResultDto()
            {
                UserID = m.UserId,
                UserName = m.UserName,
                Account = m.Account,
                Group = m.GroupId,
                Status = m.Status,
                CreateAt = m.CreateAt,
                UpdateAt = m.UpdateAt
            }).ToListAsync();
            #endregion

            return result; 
        }
    }
}
