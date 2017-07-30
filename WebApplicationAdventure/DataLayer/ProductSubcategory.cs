﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAdventure.DataLayer
{
    [Table("ProductSubcategory", Schema = "Production")]
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Product = new HashSet<Product>();
        }

        [Column("ProductSubcategoryID")]
        public int ProductSubcategoryId { get; set; }
        [Column("ProductCategoryID")]
        public int ProductCategoryId { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        public string Name { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ProductSubcategory")]
        public virtual ICollection<Product> Product { get; set; }
        [ForeignKey("ProductCategoryId")]
        [InverseProperty("ProductSubcategory")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
