using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class UserOrderInfo
    {
        public UserDTO user { get; set; }

        public OrderDTO[] orders { get; set; }
    }
}