using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models.DTO.ResultDto
{
    public class GetListAccountResultDto : PagingResultDto
    {
       public IEnumerable<AccountResultDto> data;
    }
    public class AccountResultDto
    {
        public int? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Account { get; set; }
        public int? Group { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? Status { get; set; }
    }
}
