﻿using FinalProSofra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProSofra.Models
{
    public class CartItem
    {
        public int Id { get; set; } // هذا سيكون المفتاح الأساسي

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }


}