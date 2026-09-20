using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Campus_Cafe.Models;

[Table("Product")]
[Index("Name", Name = "UQ__Product__737584F61B2D6421", IsUnique = true)]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CafeOrder> CafeOrders { get; set; } = new List<CafeOrder>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;
}
