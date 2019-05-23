using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class MyOrderViewModel
    {
        public OrderDTO[] orders { get; set; }
        public bool isguide { get; set; }
    }
}
