using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO:BaseDTO
    {
        public string OrderNum { get; set; }

        public string Name { get; set; }

        public long tid { get; set; }

        public DateTime StartDate { get; set; }

        public double Money { get; set; }

        public int state { get; set; }
    }
}
