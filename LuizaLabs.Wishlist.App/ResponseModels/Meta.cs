using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.App.ResponseModels
{
    public class Meta
    {
        public Meta() { }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int Total { get; set; }

        public void CalculateMeta(int pageNumber, int pageSize, int total)
        {
            FirstPage = 1;
            LastPage = (total % pageSize != 0) ? (total / pageSize + 1) : (total / pageSize);
            From = (pageSize * (pageNumber - 1)) + 1;
            To = (LastPage == pageNumber) ? total : pageNumber * pageSize;
            Total = total;
        }
    }
}
