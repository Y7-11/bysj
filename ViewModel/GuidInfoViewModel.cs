using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class GuidInfoViewModel
    {
      public GuideDTO guide { get; set; }

     public  LineDTO[] lines { get; set; }
    }
}
