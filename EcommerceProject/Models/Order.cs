
namespace EcommerceProject.Models
{
    public class Order:BaseModel
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public double TotalAmmount { get; set; }
        public string OrderStatus { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long Quantity { get; set; }
        public string MobileNo { get; set; }
        
      
    }
    public class GetOrder : BaseModel
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public double TotalAmmount { get; set; }
        public string OrderStatus { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long Quantity { get; set; }
        public string MobileNo { get; set; }

       
    }

   

}
