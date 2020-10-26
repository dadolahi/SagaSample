using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Core.Domain.Entities
{
   public class OrderCreateCommand
    {
        public int Id { set; get; }
        public string CustomerName { set; get; }
        public List<int> ProductIds { set; get; }
    }
}
