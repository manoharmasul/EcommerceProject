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

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
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

        public Task<long> UpdateOrdre(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
