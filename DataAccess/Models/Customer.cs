﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Table("customer", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("customer_name")]
        public string? CustomerName { get; set; }
        [Column("mobile_no")]
        public string? MobileNumber { get; set; }
        [Column("email")]
        public string? Email { get; set; }
    }
}
