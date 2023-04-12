using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IOrderAsyncRepository
    {
        Task<long> OrdreItem(Order order);
        Task<long> UpdateOrdre(Order order);
        Task<List<GetOrder>> GetMyOrders(long userid);
        Task<List<Order>> GetAllOrders();
    }
}
