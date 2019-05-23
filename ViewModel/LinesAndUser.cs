using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LinesAndUser
    {
        public UserDTO[] User { get; set; }

        public LineDTO[] Line { get; set; }
    }
}
