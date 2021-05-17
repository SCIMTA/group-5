namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<Image>();
            Rates = new HashSet<Rate>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public decimal? Price { get; set; }

        [StringLength(50)]
        public string Page { get; set; }

        [StringLength(50)]
        public string Date { get; set; }

        public int? Quantity { get; set; }

        public string Description { get; set; }

        public int? idCategory { get; set; }

        public int? idType { get; set; }

        public int? idPublisher { get; set; }

        public int? idLanguage { get; set; }

        public int? idAuthor { get; set; }

        [StringLength(100)]
        public string KeySearch { get; set; }

        public int? RatePoint { get; set; }

        public int? RateCount { get; set; }

        public int? isHidden { get; set; }

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual Language Language { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual Type Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
