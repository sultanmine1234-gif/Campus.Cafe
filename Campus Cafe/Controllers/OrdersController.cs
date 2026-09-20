using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Campus_Cafe.Models;
using Campus_Cafe.Data;
using Campus_Cafe.Dto;

namespace Campus_Cafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CampusCafeDBContext _context; 

        public OrdersController(CampusCafeDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderDto request)
        {
            if (request.Quantity < 1)
            {
                return BadRequest("Quantity must be at least 1.");
            }

            Student? foundStudent = await _context.Students
                .FirstOrDefaultAsync(student => student.StudentId == request.StudentId);

            Product? foundProduct = await _context.Products
                .FirstOrDefaultAsync(product => product.ProductId == request.ProductId);

            // Validations

            if (foundStudent == null)
            {
                return NotFound();
            }

            if (foundProduct == null)
            {
                return NotFound();
            }

            if (!foundProduct.IsAvailable)
            {
                return Conflict("The product is not available.");
            }

            // Calculations

            decimal subtotal = Math.Round(request.Quantity * foundProduct.Price,
                2,
                MidpointRounding.AwayFromZero);

            decimal discount = Math.Round(subtotal * foundStudent.DiscountPercent
                / 100m,
                2,
                MidpointRounding.AwayFromZero);

            decimal total = Math.Round(subtotal - discount,
                2,
                MidpointRounding.AwayFromZero);


            CafeOrder newOrder = new CafeOrder
            {
                StudentId = request.StudentId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                PlacedOn = DateOnly.FromDateTime(DateTime.Today),
                Subtotal = subtotal,
                Discount = discount,
                Total = total,
                Status = "Pending"
            };

            await _context.CafeOrders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return StatusCode(
                201,
                new
                {
                    newOrder.OrderId,
                    StudentName = foundStudent.FullName,
                    ProductName = foundProduct.Name,
                    newOrder.Quantity,
                    UnitPrice = foundProduct.Price,
                    newOrder.Subtotal,
                    newOrder.Discount,
                    newOrder.Total,
                    newOrder.Status
                });
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            CafeOrder? foundCafeOrder = await _context.CafeOrders
                .FirstOrDefaultAsync(order => order.OrderId == id);

            if (foundCafeOrder == null)
            {
                return NotFound();
            }

            if (foundCafeOrder.Status != "Pending")
            {
                return Conflict("Only an order whose status is 'Pending' can be cancelled");
            }

            foundCafeOrder.Status = "Cancelled";

            await _context.SaveChangesAsync();

            // QUESTION FOR TEACHER: Endpoint 8 requires 200 OK but does not specify
            // a response body. Should we return nothing, or return the order ID and status?
            return Ok();
        }
    }
}
