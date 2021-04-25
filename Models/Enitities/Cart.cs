namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        public int ID { get; set; }

        public int? idBook { get; set; }

        public int? idUser { get; set; }

        public int? Quantity { get; set; }

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}
