using Service.Models.DTO.RequestDto;
using Service.Models.DTO.ResponseDto;
namespace Service.Service.Interface
{
    public interface IAccountService
    {
        public Task<StandardResponseDto<string>> LoginAccount(LoginAccountRequestDto instance);
        public Task<StandardResponseDto<GetAccountResponseDto>> GetAccount(int id);
        public Task<StandardResponseDto<GetListAccountResponseDto>> GetListAccount(GetListAccountRequestDto instance);
        public Task<StandardResponseDto<string>> InsertAccount(PostLoginAccountRequestDto instance, string user);
        public Task<StandardResponseDto<string>> UpdateAccount(PutLoginAccountRequestDto instance, string user);
        public Task<StandardResponseDto<string>> DeleteAccount(int? id, string user);

    }
}
