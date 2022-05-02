namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("Basket")]
    public partial class Basket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Basket()
        {
            AddToBaskets = new HashSet<AddToBasket>();
        }

        public Guid ID { get; set; }

        public bool Purchased { get; set; }

        public Guid? UserID { get; set; }

        public DateTime? DateOfPurchased { get; set; }

        public string Invoice { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPrice { get; set; }

        public virtual Authentification Authentification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddToBasket> AddToBaskets { get; set; }
    }
}
