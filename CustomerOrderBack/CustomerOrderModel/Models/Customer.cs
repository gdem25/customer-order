using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderModel.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;

        [InverseProperty("Customer")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
