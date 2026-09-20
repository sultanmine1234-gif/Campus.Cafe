using Campus_Cafe.Models;
using System.ComponentModel.DataAnnotations;

namespace Campus_Cafe.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
