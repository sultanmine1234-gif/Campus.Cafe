using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Campus_Cafe.Models;

[Table("CafeOrder")]
public partial class CafeOrder
{
    [Key]
    public int OrderId { get; set; }

    public int StudentId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateOnly PlacedOn { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Discount { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Total { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("CafeOrders")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("CafeOrders")]
    public virtual Student Student { get; set; } = null!;
}
