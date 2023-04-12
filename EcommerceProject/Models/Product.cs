namespace EcommerceProject.Models
{
    public class Product:BaseModel
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public long TypeId { get; set; }
       
    }
    public class ProductAdd
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public long TypeId { get; set; }

        public List<GetProductTypes> prodtype { get; set; }
    }
    public class GetProductTypes
    {
        public long Id { get; set; }
        public string ProductType { get; set; }
    }
}
