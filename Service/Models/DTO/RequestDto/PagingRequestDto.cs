using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.DTO.RequestDto
{
    public class PagingRequestDto
    {

        public int Page { get; set; }
        public int PageOfCount { get; set; }
        public string? OrderBy { get; set; }
    }
}
