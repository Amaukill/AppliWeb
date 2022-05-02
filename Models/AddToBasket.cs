namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("AddToBasket")]
    public partial class AddToBasket
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid BasketID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Size { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string Width { get; set; }

        public bool? Returned { get; set; }

        public virtual Basket Basket { get; set; }

        public virtual Product Product { get; set; }
    }
}
