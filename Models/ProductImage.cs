namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ProductImage")]
    public partial class ProductImage
    {
        [Key]
        [StringLength(100)]
        public string ImageURL { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
