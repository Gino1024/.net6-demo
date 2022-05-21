namespace Service.Models.DTO.ResponseDto
{
    /// <summary>
    /// GetAccount回傳ViewModel
    /// </summary>
    public class GetAccountResponseDto
    {
        public AccountResponseDto data { get; set; }
    }
    /// <summary>
    /// GetListAccount回傳ViewModel
    /// </summary>
    public class GetListAccountResponseDto: PagingResponseDto
    {
        public IEnumerable<AccountResponseDto>? data { get; set; }
    }
    /// <summary>
    /// AccountViewModel
    /// </summary>
    public class AccountResponseDto
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Account { get; set; }
        public string? Group { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool? Status { get; set; }
    }
}
