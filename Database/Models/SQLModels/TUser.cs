using System;
using System.Collections.Generic;

namespace Database.Models.SQLModels
{
    public partial class TUser
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string Account { get; set; } = null!;
        public string Mima { get; set; } = null!;
        public int GroupId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? Status { get; set; }
        public string? Hash { get; set; }
    }
}
