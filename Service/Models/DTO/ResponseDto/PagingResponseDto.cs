﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.DTO.ResponseDto
{
    public class PagingResponseDto
    {
        public int? Page { get; set; }
        public int? PageOfCount { get; set; }
        public int? TotalPage { get; set; }
        public int? TotalCount { get; set; }

    }
}
