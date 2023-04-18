using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IOrderAsyncRepository
    {
        Task<long> OrdreItem(Order order);
        Task<long> UpdateOrdre(Order order);
        Task<long> UpdateOrderStatuss(UpdateOrderStatus updateordstatus);
        Task<List<GetOrder>> GetMyOrders(long userid);
        Task<List<GetOrder>> GetAllOrders();
        Task<Order> GetOrderById(long id);

    }
}
