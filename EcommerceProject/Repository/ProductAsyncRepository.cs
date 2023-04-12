using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class ProductAsyncRepository:IProductAsyncRepository
    {
        private readonly DapperContext context;
        public ProductAsyncRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<long> AddNewProduct(ProductAdd product)
        {
            //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

            var query = @"insert into tblProducts (ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedDate,IsDeleted) values(@ProductName,@ImageUrl,@Price,@Description,@Specification,@TypeId,GetDate(),0)";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query,product);

                return result;
            }
        }

        public async Task<List<ProductAdd>> GetAllProducts()
        {
            List<ProductAdd> prodlist=new List<ProductAdd>();    
            var queryprod= @"select Id,ProductName,ImageUrl,Price,Description,Specification,TypeId  from tblProducts where IsDeleted=0";
            var queryprodtype = "select * from tblProductType ";
            using (var connection=context.CreateConnection())
            {
                var products=await connection.QueryAsync<ProductAdd>(queryprod);

                prodlist=products.ToList();

                var prodtype = (await connection.QueryAsync<GetProductTypes>(queryprodtype)).ToList();

               foreach(var product in prodlist)
                {
                    //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
                    product.prodtype = prodtype;

                }
                return products.ToList();
            }
        }

        public async Task<ProductAdd> GetProductById(long id)
        {
            var query = @"select * from tblProducts where IsDeleted=0 and Id=@Id";
            using (var connection = context.CreateConnection())
            {
                 var result=await connection.QueryFirstOrDefaultAsync<ProductAdd>(query, new {Id=id});
                return result;
            }
        }

        public async Task<List<ProductAdd>> GetProductBySearchText(string searchtext)
        {
            var query = @"select * from tblProducts where (ProductName like '%'+@SearchText+'%' or @SearchText='')";
            
            using(var connection = context.CreateConnection())
            {
                var prod=await connection.QueryAsync<ProductAdd>(query, new { @SearchText=searchtext});

                return prod.ToList();

            }

        }

        public async Task<List<ProductAdd>> GetProductByType(string category)
        {
            var query = @"select * from tblProducts p inner join tblProductType pt on p.TypeId=pt.Id where pt.productType=@productType";

            using(var connection=context.CreateConnection())
            {
                var result=await connection.QueryAsync<ProductAdd>(query, new { productType= category });
                return result.ToList();
            }
        }

        public async Task<int> InsertDemo(Demo demo)
        {
            var result = 0;
            var query = @"insert into tblDemo(Demo,Price)values(@Demo,@Price) ";
           using(var connections=context.CreateConnection())
            {
              
               
                    result = await connections.ExecuteAsync(query, demo);   
                
                return result;
            }
        }

        public Task<long> UpdateProduct(ProductAdd product)
        {
            throw new NotImplementedException();
        }
    }
}
