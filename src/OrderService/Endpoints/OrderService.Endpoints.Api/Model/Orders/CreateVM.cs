using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Endpoints.Api.Model.Orders
{
    public class CreateVM
    {
        public string CustomerName { set; get; }
        public List<int> ProductIds { set; get; }
    }
}