using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakStartScreen
{
    internal interface ILinkGroup
    {
        public string Title { get; set; }
        public int Rows { get; }
        public int Count { get; }


    }
}
