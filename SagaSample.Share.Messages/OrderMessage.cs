using System.Collections.Generic;

namespace SagaSample.Share.Messages
{
    public class OrderMessage
    {
        public string CorrelationId { set; get; }
        public string State { set; get; }
        public string CustomerName { set; get; }
        public List<int> ProductIds { set; get; }
    }
}
