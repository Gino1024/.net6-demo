using System.ComponentModel.DataAnnotations;

namespace Service.Models.DTO.RequestDto
{
    public class GetListAccountRequestDto : PagingRequestDto
    {
       public string? UserName { get; set; }
       public string? Email { get; set; }
        public DateTime? CreatedAtStart { get; set; }
        public DateTime? CreatedAtEnd { get; set; }
        public DateTime? UpdateAtStart { get; set; }
        public DateTime? UpdateAtEnd { get; set; }
        public int? GroupID { get; set; }
    }
    public class LoginAccountRequestDto
    {
        [Required(ErrorMessage ="帳號為必填")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "密碼為必填")]
        public string? Mima { get; set; }
       
    }
    public class PostLoginAccountRequestDto
    {
        [Required(ErrorMessage ="使用者姓名為必填")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "信箱為必填")]
        [EmailAddress(ErrorMessage = "信箱格式錯誤")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "密碼為必填")]
        public string? Mima { get; set; }
        public int? GroupID { get; set; }
    }
    public class PutLoginAccountRequestDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Mima { get; set; }
        public int GroupID { get; set; }
        public int Status { get; set; }
    }
}
