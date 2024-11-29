using Backend.Dto;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IProductsRepository
    {
        PaginationDto<Product> GetProducts(int pageNumber, int pageSize);
        Product GetProduct(int id);

        bool ProductExists(int id);

        bool CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool Save();
    }
}
