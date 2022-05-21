using Database.Models.DTO.ParameterDto;
using Database.Models.DTO.ResultDto;
using Database.Models.SQLModels;
using Database.Repository.Interface;

namespace Database.Repository.Interface
{
    public interface IUserRepository : IGenericRepository<TUser>, IBaseRepository
    {
        Task<GetListAccountResultDto> GetListAccountAsync(GetListAccountParameterDto instance);
    }
}
