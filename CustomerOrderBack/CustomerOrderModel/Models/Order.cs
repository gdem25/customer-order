using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderModel.Models
{
    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrderName { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; } = null!;
    }
}
