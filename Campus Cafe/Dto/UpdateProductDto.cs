namespace Campus_Cafe.Dto
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
