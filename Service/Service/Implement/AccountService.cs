using Database.Repository.Interface;
using Database.UnitOfWork.Interface;
using Service.Models.DTO.RequestDto;
using Service.Models.DTO.ResponseDto;
using Service.Service.Interface;
using AutoMapper;
using Database.Models.DTO.ParameterDto;
using Database.Models.SQLModels;
using Microsoft.Extensions.Configuration;
using Infrastructrue.Implement;

namespace Service.Service.Implement
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenProvider _tokenProvider;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenProvider = tokenProvider;
        }
        public async Task<StandardResponseDto<string>> LoginAccount(LoginAccountRequestDto instance)
        {
            var result = new StandardResponseDto<string>();
            try
            {
                var user = (await _unitOfWork._userRepository.FindAsNoTrackingAsync(m => m.Account == instance.Email)).FirstOrDefault();

                if (user == null || user.Mima != instance.Mima)
                {
                    result.Message = "登入失敗";
                    return result;
                }

                var claims = new { UserId = user.UserId, UserEmail = user.Account };
                result.Result = _tokenProvider.GenerateToken(claims);
                result.Success = true;
                result.Message = "登入成功";
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 查詢使用者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StandardResponseDto<GetAccountResponseDto>> GetAccount(int id)
        {
            var result = new StandardResponseDto<GetAccountResponseDto>();
            result.Result = new GetAccountResponseDto();
            try
            {
                var data = await _unitOfWork._userRepository.GetByIdAsync(id);
                result.Result.data = _mapper.Map<AccountResponseDto>(data);
                result.Success = true;
                result.Message = "查詢成功";
            }
            catch (Exception ex)
            {
                result.Message = "查詢失敗";
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return result;
        }
        /// <summary>
        /// 查詢使用者列表
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<StandardResponseDto<GetListAccountResponseDto>> GetListAccount(GetListAccountRequestDto instance)
        {
            var result = new StandardResponseDto<GetListAccountResponseDto>();
            result.Result = new GetListAccountResponseDto();
            try
            {
              
                var parameter = _mapper.Map<GetListAccountParameterDto>(instance);
                var data = await _unitOfWork._userRepository.GetListAccountAsync(parameter);
                result.Result = _mapper.Map<GetListAccountResponseDto>(data);
                result.Success = true;
                result.Message = "查詢成功";
            }
            catch (Exception ex)
            {
                result.Message = "查詢失敗";
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }
        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<StandardResponseDto<string>> InsertAccount(PostLoginAccountRequestDto instance,string user)
        {
            var result = new StandardResponseDto<string>();
            try
            {
                var addItem = _mapper.Map<TUser>(instance);
                addItem.CreateAt = DateTime.Now;
                addItem.Status = 0;

                await _unitOfWork._userRepository.AddAsync(addItem);
                await _unitOfWork.SaveChangeAsync();
                result.Success = true;
                result.Message = "新增成功";
            }
            catch (Exception ex)
            {
                result.Message = "新增失敗";
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return result;
        }
        /// <summary>
        /// 編輯使用者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<StandardResponseDto<string>> UpdateAccount(PutLoginAccountRequestDto instance,string user)
        {
            var result = new StandardResponseDto<string>();
            try
            {
                var updateItem = await _unitOfWork._userRepository.GetByIdAsync(instance.UserId);
                if (updateItem == null)
                {
                    result.Message = "更新失敗: 無該資料";
                    return result;
                }
                updateItem.UserName = instance.UserName;
                updateItem.Mima = instance.Mima;
                updateItem.GroupId = instance.GroupID;
                updateItem.Status = instance.Status;
                updateItem.UpdateAt = DateTime.Now;
                _unitOfWork._userRepository.Update(updateItem);
                await _unitOfWork.SaveChangeAsync();
                result.Success = true;
                result.Message = "更新成功";
            }
            catch (Exception ex)
            {
                result.Message = "更新失敗";
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return result;
        }
        /// <summary>
        /// 停用使用者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<StandardResponseDto<string>> DeleteAccount(int? id, string user)
        {
            var result = new StandardResponseDto<string>();
            try
            {
                var deleteItem = await _unitOfWork._userRepository.GetByIdAsync(id.Value);
                if (deleteItem == null)
                {
                    result.Message = "刪除失敗: 無該資料";
                    return result;
                }
                _unitOfWork._userRepository.Remove(deleteItem);
                await _unitOfWork.SaveChangeAsync();

                result.Success = true;
                result.Message = "刪除成功";
            }
            catch (Exception ex)
            {
                result.Message = "刪除失敗";
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return result;
        }
    }
}
