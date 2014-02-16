using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public int Counter { get; set; }
    }

    public class ItemDBContext: DbContext
    {
        public DbSet<Item> Items { get; set; }
    }

}