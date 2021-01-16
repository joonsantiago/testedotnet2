using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LubyBackend.Utils
{
    public class Pagination<T>
    {
        public int Page { get; set; }
        public int SizePage { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { 
            get
            {
                float tc = float.Parse(TotalCount.ToString());
                float sp = float.Parse(SizePage.ToString());
                float div = tc / sp;
                return Int32.Parse(Math.Ceiling(div).ToString());
            }
        }
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
