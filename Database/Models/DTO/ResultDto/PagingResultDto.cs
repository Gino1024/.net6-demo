using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models.DTO.ResultDto
{
    public class PagingResultDto
    {
        public PagingResultDto()
        {

        }
        public void SetPaging(int totalCount,int pageOfCount, int page)
        {
            this.TotalCount = totalCount;
            this.PageOfCount = pageOfCount > 0 ? pageOfCount : 10;
            this.TotalPage = (totalCount % this.PageOfCount == 0) ? totalCount % this.PageOfCount : (totalCount / this.PageOfCount) + 1;
            this.Page = (page < 1) ? 1 : page > this.TotalPage ? totalCount : page;

        }

        public int? Page { get; set; }
        public int? PageOfCount { get; set; }
        public int? TotalPage { get; set; }
        public int? TotalCount { get; set; }

    }
}
