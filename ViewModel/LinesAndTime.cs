using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LinesAndTime
    {
        public DateTime Now { get; set; }
        public LineDTO[] Lines { get; set; }
    }
}
