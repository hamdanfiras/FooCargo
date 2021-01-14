using System;
using System.Collections.Generic;
using System.Text;

namespace FooCargo.CoreModels
{
    public class PagedResult<T>
    {
        public List<Rate> Rows { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRowCount { get; set; }
        public int RowsPerPage { get; set; }

        public int NumberOfPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalRowCount / (decimal)RowsPerPage);
            }
        }
    }
}
