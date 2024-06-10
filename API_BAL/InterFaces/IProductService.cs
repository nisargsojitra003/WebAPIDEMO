using API_DEMO_DAL.Models;
using API_DEMO_DAL.Models.ProductDTO;
using Microsoft.AspNetCore.JsonPatch;

namespace API_DEMO_BAL.InterFaces
{
    public interface IProductService
    {
        public Task<List<Product>> ProductList();
        public Task<Product> GetProductById(int productId);
        public Task<bool> CheckProduct(int productId);
        public Task<bool> CreateProductAsync(CreateProduct createProduct);
        public Task<bool> DeleteProduct(int productId);
        public Task<bool> UpdateProduct(int productId, Product updateProduct);
        public Task<bool> DeleteProductFromDb(int productId);
        public Task<bool> PartialUpdateProduct(int productId, JsonPatchDocument<Product> patchProduct);
        
    }
}
