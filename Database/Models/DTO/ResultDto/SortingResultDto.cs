using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models.DTO.ResultDto
{
    public class SortingResultDto
    {
        public SortingResultDto(string instance)
        {
            orderByList = new List<(string orderBy, bool isDec)>();
            if (!string.IsNullOrEmpty(instance))
            {
                var sortList = instance.Split(",").ToList();
                sortList.ForEach(m =>
                {
                    string columnName = m.Split(" ")[0];
                    bool isDesc = m.Split(" ")[1] == "desc" ? true : false;
                    orderByList.Add((columnName, isDesc));
                });
            }
        }
       public List<(string orderBy, bool isDec)> orderByList { get; set; }

    }
}
