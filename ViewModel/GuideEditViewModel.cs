using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace ViewModel
{
    public class GuideEditViewModel
    {
        public GuideDTO guide { get; set; }

        public string Account { get; set; }
        public string email { get; set; }
    }
}
