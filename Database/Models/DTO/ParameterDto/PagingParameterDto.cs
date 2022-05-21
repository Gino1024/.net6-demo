using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models.DTO.ParameterDto
{
    public class PagingParameterDto
    {
        public int Page { get; set; }
        public int PageOfCount { get; set; }
        public string? OrderBy { get; set; }
    }
}
