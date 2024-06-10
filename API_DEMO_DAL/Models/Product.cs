using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_DEMO_DAL.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = null!;
}
