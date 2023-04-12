using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IProductAsyncRepository
    {
        Task<long> AddNewProduct(ProductAdd product);
        Task<long> UpdateProduct(ProductAdd product);
        Task<List<ProductAdd>> GetAllProducts();
        Task<ProductAdd> GetProductById(long id);
        Task<List<ProductAdd>> GetProductByType(string category);
        Task<List<ProductAdd>> GetProductBySearchText(string searchtext);
        Task<int> InsertDemo(Demo demo);
    }
}
