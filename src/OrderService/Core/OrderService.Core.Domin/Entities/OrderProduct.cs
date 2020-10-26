namespace OrderService.Core.Domin.Entities
{
    public class OrderProduct
    {
        public int Id { set; get; }
        public int ProdcutId { set; get; }
        public int OrderId { set; get; }
        public Order Order { set; get; }
        public Product Product { set; get; }
    }
}
