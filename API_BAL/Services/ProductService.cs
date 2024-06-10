using API_DEMO_BAL.InterFaces;
using API_DEMO_DAL.DataContext;
using API_DEMO_DAL.Models;
using API_DEMO_DAL.Models.ProductDTO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace API_DEMO_BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context , IMapper mapper)
        {
            dbcontext = context;
            _mapper = mapper;
        }
        public async Task<List<Product>> ProductList()
        {
            List<Product> list = await dbcontext.Products.Where(p => p.DeletedAt == null).ToListAsync();
            return list;
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<bool> CheckProduct(int productId)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product != null ? true : false;
        }


        public async Task<bool> CreateProductAsync(CreateProduct createProduct)
        {
            if (createProduct.Name == null)
            {
                return false;
            }

            //Product newProduct = _mapper.Map<Product>(createProduct); //this method use auto mapper to add product in db
            Product newProduct = new Product()
            { 
                Name = createProduct.Name,
                CreatedAt = DateTime.Now,
                Description = createProduct.Description
            };
            await dbcontext.Products.AddAsync(newProduct);
            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if(product != null)
            {
                product.DeletedAt = DateTime.Now;
                 await dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProduct(int productId , Product updateProduct)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            
            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.ModifiedAt = DateTime.Now;
            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductFromDb(int productId)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if(product != null )
            {
                dbcontext.Products.Remove(product);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PartialUpdateProduct(int productId, JsonPatchDocument<Product> patchProduct)
        {
            Product? product = await dbcontext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                patchProduct.ApplyTo(product);
                return true;
            }
            return false;
        }
    }
}
