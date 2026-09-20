using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Campus_Cafe.Models;

[Table("Student")]
[Index("CardNumber", Name = "UQ__Student__A4E9FFE95A806BE8", IsUnique = true)]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [StringLength(80)]
    public string FullName { get; set; } = null!;

    [StringLength(10)]
    public string CardNumber { get; set; } = null!;

    public int Grade { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiscountPercent { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<CafeOrder> CafeOrders { get; set; } = new List<CafeOrder>();
}
