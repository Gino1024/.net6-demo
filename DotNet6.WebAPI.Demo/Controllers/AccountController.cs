using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Models.DTO.RequestDto;
using Service.Models.DTO.ResponseDto;
using Service.Service.Interface;
using Service.Filter;

namespace DotNet6.WebAPI.Demo.Controllers
{
    /// <summary>
    /// 使用者相關API
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    [AuthorizationFilter]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<StandardResponseDto<string>> Login([FromBody] LoginAccountRequestDto instance)
        {
            StandardResponseDto<string> result = new StandardResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    result = await _accountService.LoginAccount(instance);
                    Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Success = false;
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            
            return result;
        }
        /// <summary>
        /// 使用者單筆查詢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<StandardResponseDto<GetAccountResponseDto>> Get(int? id)
        {
            StandardResponseDto<GetAccountResponseDto> result = new StandardResponseDto<GetAccountResponseDto>();
            try
            {
                if (id.HasValue && id > 0)
                {
                    result = await _accountService.GetAccount(id.Value);
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                }
            }
            catch (Exception ex)
            {

            }
          
            return result;
        }
        /// <summary>
        /// 使用者列表查詢
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<StandardResponseDto<GetListAccountResponseDto>> Get([FromQuery] GetListAccountRequestDto instance)
        {
            StandardResponseDto<GetListAccountResponseDto> result = new StandardResponseDto<GetListAccountResponseDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    result = await _accountService.GetListAccount(instance);
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        /// <summary>
        /// 使用者新增
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<StandardResponseDto<string>> Post([FromBody] PostLoginAccountRequestDto instance)
        {
            StandardResponseDto<string> result = new StandardResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    string user = string.Empty;
                    result = await _accountService.InsertAccount(instance, user);
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        /// <summary>
        /// 使用者修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<StandardResponseDto<string>> Put([FromBody] PutLoginAccountRequestDto instance)
        {
            StandardResponseDto<string> result = new StandardResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    string user = string.Empty;
                    result = await _accountService.UpdateAccount(instance, user);
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        /// <summary>
        /// 使用者刪除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<StandardResponseDto<string>> Delete(int? id)
        {
            StandardResponseDto<string> result = new StandardResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    string user = string.Empty;
                    result = await _accountService.DeleteAccount(id, user);
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).ToList().Select(m => m.ErrorMessage);
                    result.Message = $"參數錯誤：{string.Join(',', errors)}";
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

    }
}
