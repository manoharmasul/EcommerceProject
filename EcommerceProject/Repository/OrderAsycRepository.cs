using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class OrderAsycRepository:IOrderAsyncRepository
    {
        private readonly DapperContext context;
        public OrderAsycRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<List<GetOrder>> GetAllOrders()
        {
            var query = @"select
                        ROW_NUMBER() OVER(ORDER BY o.Id desc) as SrNo,
                        o.Id,p.ProductName,o.ShippingAddress,o.OrderStatus,o.Quantity,
                        o.BillingAddress,o.CreatedDate,
                        o.MobileNo,o.TotalAmmount from tblOrder o
                        inner join tblProducts p on o.ProductId=p.Id where  o.IsDeleted=0 Order By Id Desc";
            using(var connection=context.CreateConnection())
            {
                var orders = await connection.QueryAsync<GetOrder>(query);

                return orders.ToList();
            }
        }

        public async Task<List<GetOrder>> GetMyOrders(long userid)
        {
            var query = @"select o.Id,p.ImageUrl,p.ProductName,o.ShippingAddress,o.OrderStatus,
                        o.BillingAddress,o.CreatedDate as OrderDate,
                        o.MobileNo,o.TotalAmmount from tblOrder o
                        inner join tblProducts p on o.ProductId=p.Id where CustomerId=@UserId and o.IsDeleted=0";
            using(var connection=context.CreateConnection())
            {
                var orders = await connection.QueryAsync<GetOrder>(query,new { UserId=userid });
                return orders.ToList();
            }
        }

        public async Task<Order> GetOrderById(long id)
        {
            var query = @"Select * From tblOrder Where Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.QueryAsync<Order>(query, new {Id=id});
                return result.FirstOrDefault();
            }
        }

        public async Task<long> OrdreItem(Order order)
        {
            var query = @"insert into tblOrder(CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,IsDeleted,MobileNo,Quantity) 

                         values(@CustomerId,@ProductId,@TotalAmmount,@OrderStatus,@BillingAddress,@ShippingAddress,@CreatedBy,@CreatedDate,0,@MobileNo,@Quantity)";
            using(var connection=context.CreateConnection())
            {
                order.CreatedDate=DateTime.Now;
                order.OrderStatus ="Pending";

                var result = await connection.ExecuteAsync(query, order);
                return result;  
            }
        }

        public async Task<long> UpdateOrderStatuss(UpdateOrderStatus updateordstatus)
        {
            var query = @"Update tblOrder Set OrderStatus=@OrderStatus where Id=@Id";

            using(var connection=context.CreateConnection())
            {

                var result = await connection.ExecuteAsync(query, updateordstatus);

                return result;
            }
        }

        public async Task<long> UpdateOrdre(Order order)
        {
            order.ModifiedDate = DateTime.Now;  
            var query = @"update tblOrder set	CustomerId=@CustomerId,ProductId=@ProductId,TotalAmmount=@TotalAmmount,

                       OrderStatus=@OrderStatus,BillingAddress=@BillingAddress,ShippingAddress=@ShippingAddress,

                       CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,

                       IsDeleted=@IsDeleted,Quantity=@Quantity,MobileNo=@MobileNo where Id=@Id";

            using(var connection=context.CreateConnection())
            {

                var result=await connection.ExecuteAsync(query,order);

                return result;  

            }
        }
    }
}
