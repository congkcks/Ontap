using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ontap.Models;

public partial class Product
{
    [RegularExpression(@"^[A-Z]{2}\d{4}$", ErrorMessage = "Id must be in the format XX0000 where XX are uppercase letters and 0000 are digits.")]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double UnitPrice { get; set; }

    public string? Image { get; set; }

    public bool Available { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
