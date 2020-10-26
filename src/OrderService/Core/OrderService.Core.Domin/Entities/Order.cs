using System.Collections.Generic;

namespace OrderService.Core.Domin.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public string State { set; get; }
        public string CustomerName { set; get; }
        public string CorrelationId { set; get; }
        public ICollection<OrderProduct> Products { set; get; }
    }
}
