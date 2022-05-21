using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models.DTO.ParameterDto
{
    public class GetListAccountParameterDto : PagingParameterDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAtStart { get; set; }
        public DateTime? CreatedAtEnd { get; set; }
        public DateTime? UpdateAtStart { get; set; }
        public DateTime? UpdateAtEnd { get; set; }
        public int? GroupID { get; set; }
    }
}
