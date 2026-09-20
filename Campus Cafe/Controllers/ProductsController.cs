using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Campus_Cafe.Models;
using Campus_Cafe.Data;
using Campus_Cafe.Dto;

namespace Campus_Cafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly CampusCafeDBContext _context;

        public ProductsController(CampusCafeDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(string? category)
        {
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(product =>
                product.Category.Name == category);
            }

            var products = await query
                .Select(product => new
                {
                    product.ProductId,
                    product.Name,
                    product.Price,
                    product.IsAvailable,
                    CategoryName = product.Category.Name
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetProduct(int id)
        {
            IQueryable<Product> query = _context.Products;



            var foundProduct = await query
                .Where(product => product.ProductId == id)
                .Select(product => new
                {
                    product.ProductId,
                    product.CategoryId,
                    product.Name,
                    product.Price,
                    product.IsAvailable,
                    CategoryName = product.Category.Name
                })
                .FirstOrDefaultAsync();

            if (foundProduct == null)
            {
                return NotFound();
            }

            return Ok(foundProduct);
        }

        [HttpPost]

        public async Task<ActionResult<CreateProductDto>> AddProduct(CreateProductDto request)
        {
            bool nameExists = await _context.Products
                .AnyAsync(product => product.Name == request.Name);


            bool categoryExists = await _context.Categories
                .AnyAsync(category =>
                    category.CategoryId == request.CategoryId);

            if (nameExists)
            {
                return Conflict("Name already exists");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Name is required");
            }

            if (request.Name.Length > 80)
            {
                return BadRequest("Name cannot exceed 80 characters.");
            }

            if (request.Price <= 0)
            {
                return BadRequest("Price cannot be zero or below.");
            }

            if (!categoryExists)
            {
                return BadRequest("Category Does not exist");
            }

            Product newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                IsAvailable = true
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = newProduct.ProductId },
                new
                {
                    newProduct.ProductId,
                    newProduct.Name,
                    newProduct.Price,
                    newProduct.CategoryId,
                    newProduct.IsAvailable
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateProductDto>> UpdateProduct(
            int id,
            UpdateProductDto request)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(product => product.ProductId == id);

            bool nameExists = await _context.Products
                .AnyAsync(otherProduct =>
                    otherProduct.Name == request.Name &&
                    otherProduct.ProductId != id);

            bool categoryExists = await _context.Categories
                .AnyAsync(category =>
                    category.CategoryId == request.CategoryId);

            if (product == null)
            {
                return NotFound();
            }

            if (nameExists)
            {
                return BadRequest("Name already exists");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Name is required");
            }

            if (request.Name.Length > 80)
            {
                return BadRequest("Name cannot exceed 80 characters.");
            }

            if (request.Price <= 0)
            {
                return BadRequest("Price cannot be zero or below.");
            }

            if (!categoryExists)
            {
                return BadRequest("Category Does not exist");
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.IsAvailable = request.IsAvailable;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product? Foundproduct = await _context.Products
                .FirstOrDefaultAsync(product => product.ProductId == id);

            bool orderExist = await _context.CafeOrders
                .AnyAsync(order => order.ProductId == id);

            if (Foundproduct == null)
            {
                return NotFound();
            }

            if (orderExist)
            {
                return Conflict("The product already has an order.");
            }

            _context.Products.Remove(Foundproduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
