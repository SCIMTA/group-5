namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int idOrder { get; set; }

        public int idBook { get; set; }

        public int ID { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
